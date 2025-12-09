let currentPage = 1;
let currentFilter = '';
let currentOrderBy = '';
const pageSize = 5;
let totalPages = 1;
let currentStudentId = null;

// Initialize on page load
document.addEventListener('DOMContentLoaded', function () {
    console.log('Page loaded, initializing...');
    loadStudents();
    setupEventListeners();
});

function setupEventListeners() {
    // Search input
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function (e) {
            currentFilter = e.target.value;
            currentPage = 1;
            loadStudents();
        });
    }

    // Sort select
    const sortSelect = document.getElementById('sortSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', function (e) {
            currentOrderBy = e.target.value;
            currentPage = 1;
            loadStudents();
        });
    }

    // Select all checkbox
    const selectAllCheckbox = document.getElementById('selectAll');
    if (selectAllCheckbox) {
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
    }

    // Pagination buttons
    const prevPage = document.getElementById('prevPage');
    const nextPage = document.getElementById('nextPage');

    if (prevPage) {
        prevPage.addEventListener('click', function () {
            if (currentPage > 1) {
                currentPage--;
                loadStudents();
            }
        });
    }

    if (nextPage) {
        nextPage.addEventListener('click', function () {
            if (currentPage < totalPages) {
                currentPage++;
                loadStudents();
            }
        });
    }

    // Edit form submission
    const editForm = document.getElementById('editForm');
    if (editForm) {
        editForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            await updateStudent();
        });
    }

    // Close dropdown when clicking outside
    document.addEventListener('click', function (e) {
        const dropdown = document.getElementById('actionDropdown');
        if (dropdown && !e.target.closest('.action-menu') && !e.target.closest('.action-dropdown')) {
            dropdown.style.display = 'none';
        }
    });

    // Close modals when clicking outside
    window.addEventListener('click', function (e) {
        const editModal = document.getElementById('editModal');
        const deleteModal = document.getElementById('deleteModal');

        if (e.target === editModal) {
            closeEditModal();
        }
        if (e.target === deleteModal) {
            closeDeleteModal();
        }
    });
}

async function loadStudents() {
    try {
        const url = `/api/students?filter=${encodeURIComponent(currentFilter)}&orderBy=${currentOrderBy}&pageNumber=${currentPage}&pageSize=${pageSize}`;
        console.log('Loading students from:', url);

        const response = await fetch(url);
        const data = await response.json();

        console.log('Loaded students:', data);

        displayStudents(data.students);
        updatePagination(data);
    } catch (error) {
        console.error('Error loading students:', error);
        alert('Error loading students. Please refresh the page.');
    }
}

function displayStudents(students) {
    const tbody = document.getElementById('studentsTableBody');
    if (!tbody) {
        console.error('Table body not found!');
        return;
    }

    tbody.innerHTML = '';

    students.forEach((student, index) => {
        const row = createStudentRow(student, index);
        tbody.appendChild(row);
    });

    // Reset select all checkbox
    const selectAll = document.getElementById('selectAll');
    if (selectAll) {
        selectAll.checked = false;
    }
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
            <button class="action-menu" data-student-id="${student.id}">
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
            const selectAll = document.getElementById('selectAll');
            if (selectAll) {
                selectAll.checked = false;
            }
        }
    });

    // Add action menu event listener - THIS IS THE KEY PART
    const actionButton = tr.querySelector('.action-menu');
    actionButton.addEventListener('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        console.log('Action menu clicked for student:', student.id);
        toggleActionMenu(e, student.id);
    });

    return tr;
}

function updatePagination(data) {
    totalPages = data.totalPages;
    const start = (currentPage - 1) * pageSize + 1;
    const end = Math.min(currentPage * pageSize, data.totalCount);

    // Update showing info
    const showingRange = document.getElementById('showingRange');
    const totalCount = document.getElementById('totalCount');

    if (showingRange) showingRange.textContent = `${start}-${end}`;
    if (totalCount) totalCount.textContent = data.totalCount;

    // Update prev/next buttons
    const prevPage = document.getElementById('prevPage');
    const nextPage = document.getElementById('nextPage');

    if (prevPage) prevPage.disabled = currentPage === 1;
    if (nextPage) nextPage.disabled = currentPage === totalPages;

    // Generate page numbers
    const pageNumbersContainer = document.getElementById('pageNumbers');
    if (pageNumbersContainer) {
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
}

// Action Menu Functions
function toggleActionMenu(event, studentId) {
    event.preventDefault();
    event.stopPropagation();

    console.log('Toggle action menu for student:', studentId);

    const dropdown = document.getElementById('actionDropdown');
    if (!dropdown) {
        console.error('Dropdown element not found!');
        return;
    }

    const button = event.currentTarget;
    currentStudentId = studentId;

    // Position the dropdown
    const rect = button.getBoundingClientRect();
    dropdown.style.position = 'fixed';
    dropdown.style.top = `${rect.bottom + 5}px`;
    dropdown.style.left = `${rect.left - 100}px`;
    dropdown.style.zIndex = '1000';

    // Toggle visibility
    if (dropdown.style.display === 'block') {
        dropdown.style.display = 'none';
        console.log('Hiding dropdown');
    } else {
        dropdown.style.display = 'block';
        console.log('Showing dropdown');
    }
}

async function editStudent() {
    console.log('Edit student:', currentStudentId);

    const dropdown = document.getElementById('actionDropdown');
    if (dropdown) dropdown.style.display = 'none';

    try {
        const response = await fetch(`/api/students/${currentStudentId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch student');
        }

        const student = await response.json();
        console.log('Fetched student for editing:', student);

        // Populate form
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

        // Show modal
        const editModal = document.getElementById('editModal');
        if (editModal) {
            editModal.style.display = 'block';
        }
    } catch (error) {
        console.error('Error fetching student:', error);
        alert('Error loading student data. Please try again.');
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

    console.log('Updating student:', student);

    try {
        const response = await fetch(`/api/students/${student.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(student)
        });

        if (response.ok) {
            closeEditModal();
            await loadStudents();
            alert('Student updated successfully!');
        } else {
            const error = await response.text();
            console.error('Update error:', error);
            alert('Error updating student. Please try again.');
        }
    } catch (error) {
        console.error('Error updating student:', error);
        alert('Error updating student. Please try again.');
    }
}

function closeEditModal() {
    const editModal = document.getElementById('editModal');
    if (editModal) {
        editModal.style.display = 'none';
    }
}

function openDeleteModal() {
    console.log('Open delete modal for student:', currentStudentId);

    const dropdown = document.getElementById('actionDropdown');
    if (dropdown) dropdown.style.display = 'none';

    const deleteModal = document.getElementById('deleteModal');
    if (deleteModal) {
        deleteModal.style.display = 'block';
    }
}

function closeDeleteModal() {
    const deleteModal = document.getElementById('deleteModal');
    if (deleteModal) {
        deleteModal.style.display = 'none';
    }
}

async function confirmDelete() {
    console.log('Confirming delete for student:', currentStudentId);

    try {
        const response = await fetch(`/api/students/${currentStudentId}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            closeDeleteModal();
            await loadStudents();
            alert('Student deleted successfully!');
        } else {
            const error = await response.text();
            console.error('Delete error:', error);
            alert('Error deleting student. Please try again.');
        }
    } catch (error) {
        console.error('Error deleting student:', error);
        alert('Error deleting student. Please try again.');
    }
}