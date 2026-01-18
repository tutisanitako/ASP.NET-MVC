// Auto-hide alerts after 5 seconds
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => {
        setTimeout(() => {
            alert.style.opacity = '0';
            alert.style.transform = 'translateY(-20px)';
            setTimeout(() => alert.remove(), 300);
        }, 5000);
    });

    // Dropdown functionality
    const dropdowns = document.querySelectorAll('.dropdown');
    dropdowns.forEach(dropdown => {
        const trigger = dropdown.querySelector('#userDropdown');
        const menu = dropdown.querySelector('.dropdown-menu');
        
        if (trigger && menu) {
            trigger.addEventListener('click', (e) => {
                e.preventDefault();
                menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
            });

            document.addEventListener('click', (e) => {
                if (!dropdown.contains(e.target)) {
                    menu.style.display = 'none';
                }
            });
        }
    });
});

// Smooth scroll
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

// Form validation enhancement
const forms = document.querySelectorAll('form');
forms.forEach(form => {
    form.addEventListener('submit', function(e) {
        const inputs = form.querySelectorAll('input[required], textarea[required]');
        let isValid = true;

        inputs.forEach(input => {
            if (!input.value.trim()) {
                isValid = false;
                input.style.borderColor = '#ef4444';
            } else {
                input.style.borderColor = '#f1f5f9';
            }
        });

        if (!isValid) {
            e.preventDefault();
            alert('Please fill in all required fields.');
        }
    });
});

// Add active class to current nav item
document.addEventListener('DOMContentLoaded', function() {
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('.nav-link');
    
    navLinks.forEach(link => {
        if (link.getAttribute('href') === currentPath) {
            link.style.color = '#2563eb';
            link.style.background = '#f1f5f9';
        }
    });
});

// User menu toggle
function toggleUserMenu(event) {
    event.preventDefault();
    event.stopPropagation();
    const menu = document.getElementById('userDropdownMenu');
    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
}

// Close dropdown when clicking outside
document.addEventListener('click', function (event) {
    const dropdown = document.querySelector('.user-dropdown');
    const menu = document.getElementById('userDropdownMenu');
    if (menu && dropdown && !dropdown.contains(event.target)) {
        menu.style.display = 'none';
    }
});