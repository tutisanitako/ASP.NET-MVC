const API_BASE_URL = 'https://localhost:7093/'; // Change port to match your API project

document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('createStudentForm');

    form.addEventListener('submit', async function (e) {
        e.preventDefault();

        const student = {
            firstName: document.getElementById('firstName').value,
            lastName: document.getElementById('lastName').value,
            birthDate: document.getElementById('birthDate').value,
            studentId: document.getElementById('studentId').value,
            email: document.getElementById('email').value,
            phoneNumber: document.getElementById('phoneNumber').value,
            parentName: document.getElementById('parentName').value,
            city: document.getElementById('city').value,
            grade: document.getElementById('grade').value
        };

        try {
            const response = await fetch(`${API_BASE_URL}/students`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(student)
            });

            if (response.ok) {
                // Show success modal
                document.getElementById('successModal').style.display = 'block';
            } else {
                const error = await response.json();
                alert('Error creating student: ' + JSON.stringify(error));
            }
        } catch (error) {
            console.error('Error creating student:', error);
            alert('Error creating student. Please try again.');
        }
    });
});