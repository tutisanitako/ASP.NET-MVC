let currentPage = 1;
let currentFilter = '';
let currentOrderBy = '';
const pageSize = 5;
let totalPages = 1;

// Initialize on page load
document.addEventListener('DOMContentLoaded', function () {
    loadStudents();
    setupEventListeners();
});

function setupEventListeners() {
    // Search input
    const searchInput = document.getElementById('searchInput');
    searchInput.addEventListener('input', function (e) {
        currentFilter = e.target.value;
        currentPage = 1;
        loadStudents();
    });

    // Sort select
    const sortSelect = document.getElementById('sortSelect');
    sortSelect.addEventListener('change', function (e) {
        currentOrderBy = e.target.value;
        currentPage = 1;
        loadStudents();
    });

    // Select all checkbox
    const selectAllCheckbox = document.getElementById('selectAll');
    selectAllCheckbox.addEventListener('change', function (e) {
        const checkboxes = document.querySelectorAll('.student-checkbox');
        checkboxes.forEach(checkbox => {
            checkbox.checked = e.target.checked;
            const row = checkbox.closest('tr');
            if (e.target.checked) {
                row.classList.add('selected');
            } else {
                row.classList.remove('selected');
            }
        });
    });

    // Pagination buttons
    document.getElementById('prevPage').addEventListener('click', function () {
        if (currentPage > 1) {
            currentPage--;
            loadStudents();
        }
    });

    document.getElementById('nextPage').addEventListener('click', function () {
        if (currentPage < totalPages) {
            currentPage++;
            loadStudents();
        }
    });
}

async function loadStudents() {
    try {
        const url = `/api/students?filter=${encodeURIComponent(currentFilter)}&orderBy=${currentOrderBy}&pageNumber=${currentPage}&pageSize=${pageSize}`;
        const response = await fetch(url);
        const data = await response.json();

        displayStudents(data.students);
        updatePagination(data);
    } catch (error) {
        console.error('Error loading students:', error);
    }
}

function displayStudents(students) {
    const tbody = document.getElementById('studentsTableBody');
    tbody.innerHTML = '';

    students.forEach((student, index) => {
        const row = createStudentRow(student, index);
        tbody.appendChild(row);
    });

    // Reset select all checkbox
    document.getElementById('selectAll').checked = false;
}

function createStudentRow(student, index) {
    const tr = document.createElement('tr');

    // Get initials for avatar
    const initials = student.firstName.charAt(0) + student.lastName.charAt(0);

    // Determine grade class
    const gradeClass = student.grade.includes('A') ? 'grade-a' :
        student.grade.includes('B') ? 'grade-b' : 'grade-c';

    // Format date
    const date = new Date(student.birthDate);
    const formattedDate = date.toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    });

    tr.innerHTML = `
        <td>
            <input type="checkbox" class="student-checkbox" data-id="${student.id}">
        </td>
        <td>
            <div class="student-name">
                <div class="student-avatar">${initials}</div>
                <span class="student-fullname">${student.fullName}</span>
            </div>
        </td>
        <td><span class="student-id">${student.studentId}</span></td>
        <td><span class="student-date">${formattedDate}</span></td>
        <td><span class="student-parent">${student.parentName}</span></td>
        <td><span class="student-city">${student.city}</span></td>
        <td>
            <div class="contact-icons">
                <button class="contact-icon" title="Call">
                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z"></path>
                    </svg>
                </button>
                <button class="contact-icon" title="Email">
                    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"></path>
                        <polyline points="22,6 12,13 2,6"></polyline>
                    </svg>
                </button>
            </div>
        </td>
        <td>
            <span class="grade-badge ${gradeClass}">${student.grade}</span>
        </td>
        <td>
            <button class="action-menu">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                    <circle cx="12" cy="12" r="1"></circle>
                    <circle cx="12" cy="5" r="1"></circle>
                    <circle cx="12" cy="19" r="1"></circle>
                </svg>
            </button>
        </td>
    `;

    // Add checkbox event listener
    const checkbox = tr.querySelector('.student-checkbox');
    checkbox.addEventListener('change', function (e) {
        if (e.target.checked) {
            tr.classList.add('selected');
        } else {
            tr.classList.remove('selected');
            document.getElementById('selectAll').checked = false;
        }
    });

    return tr;
}

function updatePagination(data) {
    totalPages = data.totalPages;
    const start = (currentPage - 1) * pageSize + 1;
    const end = Math.min(currentPage * pageSize, data.totalCount);

    // Update showing info
    document.getElementById('showingRange').textContent = `${start}-${end}`;
    document.getElementById('totalCount').textContent = data.totalCount;

    // Update prev/next buttons
    document.getElementById('prevPage').disabled = currentPage === 1;
    document.getElementById('nextPage').disabled = currentPage === totalPages;

    // Generate page numbers
    const pageNumbersContainer = document.getElementById('pageNumbers');
    pageNumbersContainer.innerHTML = '';

    for (let i = 1; i <= totalPages; i++) {
        const pageButton = document.createElement('button');
        pageButton.className = 'page-number';
        if (i === currentPage) {
            pageButton.classList.add('active');
        }
        pageButton.textContent = i;
        pageButton.addEventListener('click', function () {
            currentPage = i;
            loadStudents();
        });
        pageNumbersContainer.appendChild(pageButton);
    }
}