// Update this with your actual API URL - check your API project properties for the correct port
const API_BASE_URL = 'https://localhost:7093/api'; // Change 7000 to match your API port

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
            // Clear column sort indicators
            document.querySelectorAll('.sortable').forEach(h => {
                h.classList.remove('sort-asc', 'sort-desc');
            });
            loadStudents();
        });
    }

    // Setup sortable column headers
    document.querySelectorAll('.sortable').forEach(header => {
        header.addEventListener('click', function () {
            const column = this.dataset.column;
            const currentSort = this.classList.contains('sort-asc') ? 'asc' :
                this.classList.contains('sort-desc') ? 'desc' : 'none';

            // Remove sort classes from all headers
            document.querySelectorAll('.sortable').forEach(h => {
                h.classList.remove('sort-asc', 'sort-desc');
            });

            // Clear the dropdown
            const sortSelect = document.getElementById('sortSelect');
            if (sortSelect) sortSelect.value = '';

            // Determine new sort direction
            if (currentSort === 'none' || currentSort === 'desc') {
                this.classList.add('sort-asc');
                currentOrderBy = column + '_asc';
            } else {
                this.classList.add('sort-desc');
                currentOrderBy = column + '_desc';
            }

            currentPage = 1;
            loadStudents();
        });
    });

    const selectAllCheckbox = document.getElementById('selectAll');
    if (selectAllCheckbox) {
        selectAllCheckbox.addEventListener('change', function () {
            document.querySelectorAll('.student-checkbox').forEach(cb => {
                cb.checked = this.checked;
                cb.closest('tr').classList.toggle('selected', this.checked);
            });
        });
    }

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
    const url = `${API_BASE_URL}/students?filter=${encodeURIComponent(currentFilter)}&orderBy=${currentOrderBy}&pageNumber=${currentPage}&pageSize=${pageSize}`;

    try {
        const res = await fetch(url);
        const data = await res.json();

        displayStudents(data.students || []);
        updatePagination(data);
    } catch (err) {
        console.error('Error loading students:', err);
        alert("Failed to load students. Make sure the API is running at " + API_BASE_URL);
    }
}

function displayStudents(students) {
    const tbody = document.getElementById('studentsTableBody');
    tbody.innerHTML = '';

    if (students.length === 0) {
        tbody.innerHTML = '<tr><td colspan="9" style="text-align: center; padding: 40px; color: #8E8E93;">No students found</td></tr>';
        return;
    }

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
                    <button class="contact-icon" title="Call ${student.phoneNumber}">
                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                            <path d="M22 16.92v3a2 2 0 0 1-2.18 2 19.79 19.79 0 0 1-8.63-3.07 19.5 19.5 0 0 1-6-6 19.79 19.79 0 0 1-3.07-8.67A2 2 0 0 1 4.11 2h3a2 2 0 0 1 2 1.72 12.84 12.84 0 0 0 .7 2.81 2 2 0 0 1-.45 2.11L8.09 9.91a16 16 0 0 0 6 6l1.27-1.27a2 2 0 0 1 2.11-.45 12.84 12.84 0 0 0 2.81.7A2 2 0 0 1 22 16.92z"></path>
                        </svg>
                    </button>
                    <button class="contact-icon" title="Email ${student.email}">
                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                            <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"></path>
                            <polyline points="22,6 12,13 2,6"></polyline>
                        </svg>
                    </button>
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
    const selectAllCheckbox = document.getElementById('selectAll');
    if (selectAllCheckbox) {
        selectAllCheckbox.checked = all.length > 0 && all.length === checked.length;
    }
}

function toggleActionMenu(event, studentId) {
    event.stopPropagation();
    currentStudentId = studentId;

    const dropdown = document.getElementById('actionDropdown');
    const btn = event.currentTarget;
    const rect = btn.getBoundingClientRect();

    dropdown.style.display = 'block';
    dropdown.style.top = `${rect.bottom + window.scrollY + 5}px`;
    dropdown.style.left = `${rect.right + window.scrollX - dropdown.offsetWidth}px`;
}

async function editStudent() {
    document.getElementById('actionDropdown').style.display = 'none';
    try {
        const res = await fetch(`${API_BASE_URL}/students/${currentStudentId}`);
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
        alert("Failed to load student data.");
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

    try {
        const res = await fetch(`${API_BASE_URL}/students/${student.id}`, {
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
        const res = await fetch(`${API_BASE_URL}/students/${currentStudentId}`, { method: 'DELETE' });
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