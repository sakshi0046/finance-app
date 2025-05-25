// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/**
 * Premium FinancialApp - Enhanced JavaScript Functionality
 * Adds modern interactive features, animations, and user experience improvements
 */

// Wait for DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function () {

    // Initialize UI components
    initializeNavigation();
    initializeAnimations();
    initializeDarkMode();
    initializeTooltips();
    initializeRippleEffect();
    initializeNotifications();

    // Add page transition effect
    addPageTransitionEffect();

    // Charts and other special functionality
    if (document.querySelector('.dashboard-stats')) {
        initializeDashboardCharts();
    }
});

/**
 * Initialize navigation-related functionality
 */
function initializeNavigation() {
    // Set active class on current navigation item
    const currentPath = window.location.pathname.toLowerCase();
    const navLinks = document.querySelectorAll('.nav-link');

    navLinks.forEach(link => {
        const href = link.getAttribute('href')?.toLowerCase();
        if (href && href !== '#' && currentPath.includes(href) && href !== '/') {
            link.classList.add('active');

            // If in a dropdown, also highlight parent
            const parentDropdown = link.closest('.dropdown');
            if (parentDropdown) {
                const dropdownToggle = parentDropdown.querySelector('.dropdown-toggle');
                if (dropdownToggle) {
                    dropdownToggle.classList.add('active');
                }
            }
        } else if (href === '/' && (currentPath === '/' || currentPath === '/home' || currentPath === '/home/index')) {
            link.classList.add('active');
        }
    });

    // Enhanced dropdown behavior on desktop
    if (window.innerWidth >= 992) {
        const dropdowns = document.querySelectorAll('.dropdown');

        dropdowns.forEach(dropdown => {
            const dropdownMenu = dropdown.querySelector('.dropdown-menu');
            const dropdownToggle = dropdown.querySelector('.dropdown-toggle');

            dropdown.addEventListener('mouseenter', function () {
                dropdownMenu.classList.add('show');
                dropdownToggle.setAttribute('aria-expanded', 'true');
            });

            dropdown.addEventListener('mouseleave', function () {
                dropdownMenu.classList.remove('show');
                dropdownToggle.setAttribute('aria-expanded', 'false');
            });
        });
    }

    // Smooth scroll for anchor links
    document.querySelectorAll('a[href^="#"]:not([data-bs-toggle])').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            const targetId = this.getAttribute('href');
            if (targetId === '#') return;

            const targetElement = document.querySelector(targetId);
            if (targetElement) {
                e.preventDefault();
                window.scrollTo({
                    top: targetElement.offsetTop - 80, // Account for navbar height
                    behavior: 'smooth'
                });
            }
        });
    });
}

/**
 * Initialize animations and effects
 */
function initializeAnimations() {
    // Animate elements when they come into view
    const animatedElements = document.querySelectorAll('.animate-on-scroll');

    if (animatedElements.length > 0) {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('animated');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        animatedElements.forEach(element => {
            observer.observe(element);
        });
    }

    // Number counter animation
    const countElements = document.querySelectorAll('.count-up');

    if (countElements.length > 0) {
        const counterObserver = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const target = entry.target;
                    const countTo = parseInt(target.getAttribute('data-count'));

                    if (isNaN(countTo)) return;

                    let current = 0;
                    const increment = Math.ceil(countTo / 50);
                    const duration = 1500;
                    const step = Math.floor(duration / (countTo / increment));

                    const timer = setInterval(() => {
                        current += increment;
                        target.textContent = current.toLocaleString();

                        if (current >= countTo) {
                            target.textContent = countTo.toLocaleString();
                            clearInterval(timer);
                        }
                    }, step);

                    counterObserver.unobserve(target);
                }
            });
        }, { threshold: 0.5 });

        countElements.forEach(element => {
            counterObserver.observe(element);
        });
    }
}

/**
 * Initialize dark mode toggle functionality
 */
function initializeDarkMode() {
    const darkModeToggle = document.getElementById('darkModeToggle');
    if (!darkModeToggle) return;

    const body = document.body;
    const icon = darkModeToggle.querySelector('i');

    // Check for saved theme preference
    const savedTheme = localStorage.getItem('theme');

    // Apply saved theme on page load
    if (savedTheme === 'dark') {
        body.classList.add('dark-theme');
        if (icon) {
            icon.classList.remove('fa-moon');
            icon.classList.add('fa-sun');
        }
        applyDarkModeColors(true);
    }

    // Toggle dark/light mode
    darkModeToggle.addEventListener('click', function () {
        if (body.classList.contains('dark-theme')) {
            body.classList.remove('dark-theme');
            if (icon) {
                icon.classList.remove('fa-sun');
                icon.classList.add('fa-moon');
            }
            localStorage.setItem('theme', 'light');
            applyDarkModeColors(false);
        } else {
            body.classList.add('dark-theme');
            if (icon) {
                icon.classList.remove('fa-moon');
                icon.classList.add('fa-sun');
            }
            localStorage.setItem('theme', 'dark');
            applyDarkModeColors(true);
        }
    });
}

/**
 * Apply theme-specific CSS variable overrides
 */
function applyDarkModeColors(isDark) {
    if (isDark) {
        document.documentElement.style.setProperty('--light-bg', '#1a1a2e');
        document.documentElement.style.setProperty('--white', '#16213e');
        document.documentElement.style.setProperty('--dark-text', '#e2e8f0');
        document.documentElement.style.setProperty('--medium-text', '#cbd5e1');
        document.documentElement.style.setProperty('--light-text', '#94a3b8');
    } else {
        document.documentElement.style.setProperty('--light-bg', '#f9fafb');
        document.documentElement.style.setProperty('--white', '#ffffff');
        document.documentElement.style.setProperty('--dark-text', '#1f2937');
        document.documentElement.style.setProperty('--medium-text', '#374151');
        document.documentElement.style.setProperty('--light-text', '#6b7280');
    }
}

/**
 * Initialize Bootstrap tooltips and custom tooltips
 */
function initializeTooltips() {
    // Initialize Bootstrap tooltips if available
    if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
        const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
        [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));
    }
}

/**
 * Add ripple effect to buttons and interactive elements
 */
function initializeRippleEffect() {
    const buttons = document.querySelectorAll('.btn, .nav-link, .dropdown-item');

    buttons.forEach(button => {
        button.addEventListener('click', function (e) {
            // Check if element already has a ripple span
            const existingRipple = this.querySelector('.ripple');
            if (existingRipple) {
                existingRipple.remove();
            }

            const ripple = document.createElement('span');
            ripple.classList.add('ripple');

            // Calculate position
            const rect = this.getBoundingClientRect();
            const size = Math.max(rect.width, rect.height);

            ripple.style.width = ripple.style.height = `${size}px`;
            ripple.style.left = `${e.clientX - rect.left - size / 2}px`;
            ripple.style.top = `${e.clientY - rect.top - size / 2}px`;

            this.appendChild(ripple);

            // Remove ripple element after animation completes
            setTimeout(() => {
                if (ripple) {
                    ripple.remove();
                }
            }, 600);
        });
    });
}

/**
 * Add page transition effects
 */
function addPageTransitionEffect() {
    // Fade-in effect for main content
    const mainContent = document.querySelector('main');
    if (mainContent) {
        mainContent.classList.add('fade-in');
    }

    // Transition effect when navigating to a new page
    document.querySelectorAll('a:not([target="_blank"]):not([data-bs-toggle])').forEach(link => {
        link.addEventListener('click', function (e) {
            // Skip for special cases
            if (
                this.getAttribute('href') === '#' ||
                this.getAttribute('href')?.startsWith('#') ||
                e.ctrlKey ||
                e.metaKey ||
                this.getAttribute('download') !== null
            ) {
                return;
            }

            // Skip if it's within a dropdown
            if (this.classList.contains('dropdown-toggle')) {
                return;
            }

            const mainContent = document.querySelector('main');
            if (mainContent) {
                e.preventDefault();
                mainContent.style.opacity = '0';
                mainContent.style.transform = 'translateY(10px)';

                setTimeout(() => {
                    window.location.href = this.href;
                }, 200);
            }
        });
    });
}

/**
 * Initialize notification system
 */
function initializeNotifications() {
    // Toast notification function - can be called from anywhere
    window.showToast = function (message, type = 'info', duration = 3000) {
        // Create toast container if it doesn't exist
        let toastContainer = document.getElementById('toast-container');
        if (!toastContainer) {
            toastContainer = document.createElement('div');
            toastContainer.id = 'toast-container';
            toastContainer.className = 'position-fixed bottom-0 end-0 p-3';
            toastContainer.style.zIndex = '1050';
            document.body.appendChild(toastContainer);
        }

        // Create the toast element
        const toastId = 'toast-' + Date.now();
        const toastEl = document.createElement('div');
        toastEl.id = toastId;
        toastEl.className = `toast align-items-center text-white bg-${type} border-0`;
        toastEl.setAttribute('role', 'alert');
        toastEl.setAttribute('aria-live', 'assertive');
        toastEl.setAttribute('aria-atomic', 'true');

        // Toast content
        toastEl.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        `;

        toastContainer.appendChild(toastEl);

        // Initialize and show the toast
        const toast = new bootstrap.Toast(toastEl, {
            autohide: true,
            delay: duration
        });

        toast.show();

        // Remove toast from DOM after it's hidden
        toastEl.addEventListener('hidden.bs.toast', function () {
            toastEl.remove();
        });
    };
}

/**
 * Initialize dashboard charts and stats (if present)
 */
function initializeDashboardCharts() {
    // Example: Initialize charts if the page contains dashboard charts
    // This is a placeholder - actual implementation would depend on your specific charts

    if (typeof Chart !== 'undefined') {
        // Initialize expense distribution chart
        const expenseCtx = document.getElementById('expenseDistributionChart');
        if (expenseCtx) {
            new Chart(expenseCtx, {
                type: 'doughnut',
                data: {
                    // Chart data would come from your backend
                    labels: ['Housing', 'Food', 'Transportation', 'Utilities', 'Entertainment', 'Other'],
                    datasets: [{
                        data: [35, 25, 15, 10, 10, 5],
                        backgroundColor: [
                            '#3b82f6', '#10b981', '#f59e0b',
                            '#ef4444', '#8b5cf6', '#6b7280'
                        ]
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'bottom'
                        }
                    }
                }
            });
        }

        // Initialize income vs expenses chart
        const incomeExpenseCtx = document.getElementById('incomeExpenseChart');
        if (incomeExpenseCtx) {
            new Chart(incomeExpenseCtx, {
                type: 'bar',
                data: {
                    // Chart data would come from your backend
                    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                    datasets: [
                        {
                            label: 'Income',
                            data: [4500, 4200, 4800, 5000, 4700, 5200],
                            backgroundColor: '#10b981'
                        },
                        {
                            label: 'Expenses',
                            data: [3800, 3600, 4100, 3900, 4200, 4300],
                            backgroundColor: '#ef4444'
                        }
                    ]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        }
    }
}

/**
 * Form validation and enhancements
 */
document.addEventListener('DOMContentLoaded', function () {
    // Enhanced form validation
    const forms = document.querySelectorAll('.needs-validation');

    forms.forEach(form => {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();

                // Animate invalid fields
                form.querySelectorAll(':invalid').forEach(field => {
                    field.classList.add('shake-animation');
                    setTimeout(() => {
                        field.classList.remove('shake-animation');
                    }, 600);
                });
            }

            form.classList.add('was-validated');
        });

        // Live validation feedback
        form.querySelectorAll('input, select, textarea').forEach(field => {
            field.addEventListener('blur', function () {
                if (this.checkValidity()) {
                    this.classList.add('is-valid');
                    this.classList.remove('is-invalid');
                } else if (this.value !== '') {
                    this.classList.add('is-invalid');
                    this.classList.remove('is-valid');
                }
            });
        });
    });

    // Currency input formatting
    document.querySelectorAll('.currency-input').forEach(input => {
        input.addEventListener('input', function () {
            let value = this.value.replace(/[^\d.]/g, '');

            // Format with 2 decimal places
            if (value) {
                value = parseFloat(value).toFixed(2);
                this.value = new Intl.NumberFormat('en-US', {
                    style: 'currency',
                    currency: 'USD',
                    minimumFractionDigits: 2
                }).format(value);
            }
        });
    });
});

// Add shake animation for invalid form fields
const style = document.createElement('style');
style.textContent = `
@keyframes shake {
    0%, 100% { transform: translateX(0); }
    20%, 60% { transform: translateX(-5px); }
    40%, 80% { transform: translateX(5px); }
}
.shake-animation {
    animation: shake 0.6s ease-in-out;
}
`;
document.head.appendChild(style);