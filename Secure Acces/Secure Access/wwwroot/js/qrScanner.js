let video = null;
let scanning = false;
let stream = null;
let statusEl = null;

document.addEventListener('DOMContentLoaded', () => {
    video = document.getElementById('camera');
    statusEl = document.getElementById('status');

    document.getElementById('startBtn').addEventListener('click', startScanner);
    document.getElementById('stopBtn').addEventListener('click', stopScanner);
});

async function startScanner() {
    try {
        stream = await navigator.mediaDevices.getUserMedia({ video: { facingMode: "environment" } });
        video.srcObject = stream;
        video.setAttribute("playsinline", true);
        await video.play();

        scanning = true;
        toggleButtons(true);
        statusEl.textContent = "Scanning for QR codes...";
        requestAnimationFrame(scanFrame);
    } catch (err) {
        statusEl.textContent = "Camera access error: " + err.message;
    }
}

function stopScanner() {
    scanning = false;
    if (stream) {
        stream.getTracks().forEach(t => t.stop());
        stream = null;
    }
    toggleButtons(false);
    statusEl.textContent = "Scanner stopped.";
}

function toggleButtons(isScanning) {
    document.getElementById('startBtn').style.display = isScanning ? 'none' : 'inline-block';
    document.getElementById('stopBtn').style.display = isScanning ? 'inline-block' : 'none';
}

function scanFrame() {
    if (!scanning) return;

    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d');
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    const imgData = ctx.getImageData(0, 0, canvas.width, canvas.height);
    const code = jsQR(imgData.data, imgData.width, imgData.height);

    if (code) {
        scanning = false;
        stopScanner();
        const token = extractToken(code.data);
        sendToServer(token);
    } else {
        requestAnimationFrame(scanFrame);
    }
}

function extractToken(qrData) {
    try {
        const url = new URL(qrData);
        return url.searchParams.get("token") || qrData;
    } catch {
        return qrData;
    }
}

async function sendToServer(token) {
    try {
        const response = await fetch(`/QR/Scan?token=${encodeURIComponent(token)}`);
        const result = await response.text();
        statusEl.innerHTML = `<strong>${result}</strong>`;
    } catch (err) {
        statusEl.innerHTML = `<span style="color:red;">Error sending QR code: ${err.message}</span>`;
    }
}
