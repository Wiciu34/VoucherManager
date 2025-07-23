class Toast {
    constructor() {
        if (!document.getElementById('toast-container')) {
            const container = document.createElement('div');
            container.id = 'toast-container';
            container.classList.add('toast-container', 'position-fixed', 'top-0', 'end-0', 'p-3');
            container.style.zIndex = '1100';
            document.body.appendChild(container);
        }
    }


    show(message, type = 'success', duration = 3000) {
        const toast = document.createElement('div');
        toast.classList.add('toast', 'align-items-center', 'border-0', `bg-${type}`, 'text-white');
        toast.setAttribute('role', 'alert');
        toast.setAttribute('aria-live', 'assertLive');
        toast.setAttribute('aria-atomic', 'true');

        toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        `;

        document.getElementById('toast-container').appendChild(toast);

        const bootstrapToast = new bootstrap.Toast(toast, { delay: duration });
        bootstrapToast.show();

        toast.addEventListener('hidden.bs.toast', () => toast.remove());
    }
}

const toast = new Toast();