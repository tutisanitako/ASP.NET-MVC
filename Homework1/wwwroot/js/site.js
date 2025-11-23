// Recipe Like and Bookmark functionality
document.addEventListener('DOMContentLoaded', function () {
    // Handle Like buttons
    const likeButtons = document.querySelectorAll('.like-btn');
    likeButtons.forEach(button => {
        button.addEventListener('click', function () {
            const recipeId = this.getAttribute('data-id');
            toggleLike(recipeId, this);
        });
    });

    // Handle Bookmark buttons
    const bookmarkButtons = document.querySelectorAll('.bookmark-btn');
    bookmarkButtons.forEach(button => {
        button.addEventListener('click', function () {
            const recipeId = this.getAttribute('data-id');
            toggleBookmark(recipeId, this);
        });
    });
});

function toggleLike(recipeId, button) {
    fetch('/Recipe/ToggleLike', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': getAntiForgeryToken()
        },
        body: JSON.stringify({ id: recipeId })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                const span = button.querySelector('span');
                if (data.isLiked) {
                    span.classList.add('liked');
                } else {
                    span.classList.remove('liked');
                }
            }
        })
        .catch(error => console.error('Error:', error));
}

function toggleBookmark(recipeId, button) {
    fetch('/Recipe/ToggleBookmark', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': getAntiForgeryToken()
        },
        body: JSON.stringify({ id: recipeId })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                const span = button.querySelector('span');
                if (data.isBookmarked) {
                    span.classList.add('bookmarked');
                } else {
                    span.classList.remove('bookmarked');
                }
            }
        })
        .catch(error => console.error('Error:', error));
}

function getAntiForgeryToken() {
    const token = document.querySelector('input[name="__RequestVerificationToken"]');
    return token ? token.value : '';
}

// Auto-hide success messages after 5 seconds
const alerts = document.querySelectorAll('.alert-success');
if (alerts.length > 0) {
    setTimeout(() => {
        alerts.forEach(alert => {
            alert.style.opacity = '0';
            alert.style.transition = 'opacity 0.5s';
            setTimeout(() => alert.remove(), 500);
        });
    }, 5000);
}