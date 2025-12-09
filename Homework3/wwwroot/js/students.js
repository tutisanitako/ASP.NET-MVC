// wwwroot/js/students.js
let currentPage = 1;
let currentFilter = '';
let currentOrderBy = '';
const pageSize = 5;
let totalPages = 1;
let currentStudentId = null;

document.addEventListener('DOMContentLoaded', function () {
    loadStudents();
    setupEventListeners();
});

function setupEventListeners() {
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', debounce(function (e) {
            currentFilter = e.target.value.trim();
            currentPage = 1;
            loadStudents();
        }, 300));
    }

    const sortSelect = document.getElementById('sortSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', function (e) {
            currentOrderBy = e.target.value;
            currentPage = 1;
            loadStudents();
        });
    }

    const selectAllCheckbox = document.getElementById('selectAll');
    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function () {
            document.querySelectorAll('.student-checkbox').forEach(cb => {
                cb.checked = this.checked;
                cb.closest('tr').classList.toggle('selected', this.checked);
            });
        });
    }

    // FIX: Add edit form submit handler
    const editForm = document.getElementById('editForm');
    if (editForm) {
        editForm.addEventListener('submit', function (e) {
            e.preventDefault();
            updateStudent();
        });
    }

    document.getElementById('prevPage')?.addEventListener('click', () => {
        if (currentPage > 1) { currentPage--; loadStudents(); }
    });

    document.getElementById('nextPage')?.addEventListener('click', () => {
        if (currentPage < totalPages) { currentPage++; loadStudents(); }
    });

    // Close dropdown/modals when clicking outside
    document.addEventListener('click', function (e) {
        const dropdown = document.getElementById('actionDropdown');
        if (dropdown && !e.target.closest('.action-menu') && !e.target.closest('.action-dropdown')) {
            dropdown.style.display = 'none';
        }
        if (e.target.classList.contains('modal')) {
            e.target.style.display = 'none';
        }
    });
}

// Debounce helper
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

async function loadStudents() {
    const url = `/api/students?filter=${encodeURIComponent(currentFilter)}&orderBy=${currentOrderBy}&pageNumber=${currentPage}&pageSize=${pageSize}`;
    try {
        const res = await fetch(url);
        const data = await res.json();

        displayStudents(data.students || []);
        updatePagination(data);
    } catch (err) {
        console.error(err);
        alert("Failed to load students.");
    }
}

function displayStudents(students) {
    const tbody = document.getElementById('studentsTableBody');
    tbody.innerHTML = '';

    students.forEach(student => {
        const initials = (student.firstName[0] + student.lastName[0]).toUpperCase();
        const date = new Date(student.birthDate).toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
        const gradeClass = student.grade.includes('A') ? 'grade-a' : student.grade.includes('B') ? 'grade-b' : 'grade-c';

        const row = document.createElement('tr');
        row.innerHTML = `
            <td><input type="checkbox" class="student-checkbox" data-id="${student.id}"></td>
            <td>
                <div class="student-name">
                    <div class="student-avatar">${initials}</div>
                    <span class="student-fullname">${student.firstName} ${student.lastName}</span>
                </div>
            </td>
            <td><span class="student-id">${student.studentId}</span></td>
            <td><span class="student-date">${date}</span></td>
            <td><span class="student-parent">${student.parentName}</span></td>
            <td><span class="student-city">${student.city}</span></td>
            <td>
                <div class="contact-icons">
                    <button class="contact-icon" title="Call"><svg><!-- phone icon --></svg></button>
                    <button class="contact-icon" title="Email"><svg><!-- email icon --></svg></button>
                </div>
            </td>
            <td><span class="grade-badge ${gradeClass}">${student.grade}</span></td>
            <td>
                <button class="action-menu" onclick="toggleActionMenu(event, ${student.id})">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                        <circle cx="12" cy="12" r="1"></circle>
                        <circle cx="12" cy="5" r="1"></circle>
                        <circle cx="12" cy="19" r="1"></circle>
                    </svg>
                </button>
            </td>
        `;

        // Checkbox selection
        row.querySelector('.student-checkbox').addEventListener('change', function () {
            this.closest('tr').classList.toggle('selected', this.checked);
            updateSelectAllCheckbox();
        });

        tbody.appendChild(row);
    });
}

function updateSelectAllCheckbox() {
    const all = document.querySelectorAll('.student-checkbox');
    const checked = document.querySelectorAll('.student-checkbox:checked');
    document.getElementById('selectAll').checked = all.length > 0 && all.length === checked.length;
}

function toggleActionMenu(event, studentId) {
    event.stopPropagation();
    currentStudentId = studentId;

    const dropdown = document.getElementById('actionDropdown');
    const btn = event.currentTarget;
    const rect = btn.getBoundingClientRect();

    dropdown.style.display = 'block';
    dropdown.style.top = `${rect.bottom + 5}px`;
    dropdown.style.left = `${rect.right - dropdown.offsetWidth}px`;
}

async function editStudent() {
    document.getElementById('actionDropdown').style.display = 'none';
    try {
        const res = await fetch(`/api/students/${currentStudentId}`);
        const student = await res.json();

        document.getElementById('editId').value = student.id;
        document.getElementById('editFirstName').value = student.firstName;
        document.getElementById('editLastName').value = student.lastName;
        document.getElementById('editBirthDate').value = student.birthDate.split('T')[0];
        document.getElementById('editStudentId').value = student.studentId;
        document.getElementById('editEmail').value = student.email;
        document.getElementById('editPhoneNumber').value = student.phoneNumber;
        document.getElementById('editParentName').value = student.parentName;
        document.getElementById('editCity').value = student.city;
        document.getElementById('editGrade').value = student.grade;

        document.getElementById('editModal').style.display = 'block';
    } catch (err) {
        console.error('Error loading student:', err);
        alert("Failed to load student.");
    }
}

async function updateStudent() {
    const student = {
        id: parseInt(document.getElementById('editId').value),
        firstName: document.getElementById('editFirstName').value,
        lastName: document.getElementById('editLastName').value,
        birthDate: document.getElementById('editBirthDate').value,
        studentId: document.getElementById('editStudentId').value,
        email: document.getElementById('editEmail').value,
        phoneNumber: document.getElementById('editPhoneNumber').value,
        parentName: document.getElementById('editParentName').value,
        city: document.getElementById('editCity').value,
        grade: document.getElementById('editGrade').value
    };

    console.log('Updating student:', student); // Debug log

    try {
        const res = await fetch(`/api/students/${student.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(student)
        });

        if (res.ok) {
            document.getElementById('editModal').style.display = 'none';
            loadStudents();
            alert('Student updated successfully!');
        } else {
            const error = await res.text();
            console.error('Update failed:', error);
            alert("Failed to update student: " + error);
        }
    } catch (err) {
        console.error('Error updating student:', err);
        alert("Error updating student.");
    }
}

function openDeleteModal() {
    document.getElementById('actionDropdown').style.display = 'none';
    document.getElementById('deleteModal').style.display = 'block';
}

async function confirmDelete() {
    try {
        const res = await fetch(`/api/students/${currentStudentId}`, { method: 'DELETE' });
        if (res.ok) {
            document.getElementById('deleteModal').style.display = 'none';
            loadStudents();
            alert('Student deleted successfully!');
        } else {
            alert("Failed to delete student.");
        }
    } catch (err) {
        console.error('Error deleting student:', err);
        alert("Failed to delete student.");
    }
}

function closeEditModal() {
    document.getElementById('editModal').style.display = 'none';
}

function closeDeleteModal() {
    document.getElementById('deleteModal').style.display = 'none';
}

function updatePagination(data) {
    totalPages = data.totalPages || 1;
    const start = (currentPage - 1) * pageSize + 1;
    const end = Math.min(currentPage * pageSize, data.totalCount);

    document.getElementById('showingRange').textContent = `${start}-${end}`;
    document.getElementById('totalCount').textContent = data.totalCount;

    document.getElementById('prevPage').disabled = currentPage === 1;
    document.getElementById('nextPage').disabled = currentPage === totalPages;

    const container = document.getElementById('pageNumbers');
    container.innerHTML = '';
    for (let i = 1; i <= totalPages; i++) {
        const btn = document.createElement('button');
        btn.className = 'page-number';
        if (i === currentPage) btn.classList.add('active');
        btn.textContent = i;
        btn.onclick = () => { currentPage = i; loadStudents(); };
        container.appendChild(btn);
    }
}