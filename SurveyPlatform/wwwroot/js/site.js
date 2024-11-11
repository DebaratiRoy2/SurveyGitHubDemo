function showAlert(message) {
    const alertBox = document.createElement('div');
    alertBox.className = 'alert-box';
    alertBox.innerHTML = `
        <div class="alert-content">
            <span class="close-btn" onclick="this.parentElement.parentElement.remove();">&times;</span>
            <p>${message}</p>
        </div>
    `;
    document.body.appendChild(alertBox);
}

// CSS for the alert box
const style = document.createElement('style');
style.innerHTML = `
    .alert-box {
        position: fixed;
        top: 20px;
        right: 20px;
        background-color: #f44336;
        color: white;
        padding: 20px;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        z-index: 1000;
    }
    .alert-content {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
    .close-btn {
        cursor: pointer;
        font-size: 20px;
        margin-left: 20px;
    }
`;
document.head.appendChild(style);
