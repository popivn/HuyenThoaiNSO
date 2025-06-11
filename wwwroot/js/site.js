// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function() {
    const themeToggle = document.getElementById('theme-toggle');
    if (!themeToggle) {
        console.error('Theme toggle button not found!');
        return;
    }

    const themeIcon = themeToggle.querySelector('.theme-icon');
    if (!themeIcon) {
        console.error('Theme icon not found!');
        return;
    }

    // Check for saved theme preference
    const isDarkMode = localStorage.getItem('darkMode') === 'true';
    
    // Apply theme on page load
    if (isDarkMode) {
        document.body.classList.add('dark-mode');
        themeIcon.classList.remove('bi-moon-fill');
        themeIcon.classList.add('bi-sun-fill');
    } else {
        document.body.classList.remove('dark-mode');
        themeIcon.classList.remove('bi-sun-fill');
        themeIcon.classList.add('bi-moon-fill');
    }

    // Toggle theme on click
    themeToggle.addEventListener('click', function() {
        const isDark = document.body.classList.toggle('dark-mode');
        localStorage.setItem('darkMode', isDark);

        if (isDark) {
            themeIcon.classList.remove('bi-moon-fill');
            themeIcon.classList.add('bi-sun-fill');
        } else {
            themeIcon.classList.remove('bi-sun-fill');
            themeIcon.classList.add('bi-moon-fill');
        }
    });
});