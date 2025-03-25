document.addEventListener('DOMContentLoaded', () => {
  const form = document.getElementById('password-form');
  const alertContainer = document.getElementById('alert-container');
  const urlParams = new URLSearchParams(window.location.search);
  const token = urlParams.get('token');

  form.addEventListener('submit', async (event) => {
      event.preventDefault();

      const newPassword = document.getElementById('new-password').value;
      const confirmPassword = document.getElementById('confirm-password').value;

      if (newPassword !== confirmPassword) {
          showAlert('Las contraseñas no coinciden', 'error');
          return;
      }

      try {
          const response = await fetch('https://localhost:5223/api/user/confirm-reset-password', {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json'
              },
              body: JSON.stringify({
                  token: token,
                  newPassword: newPassword
              })
          });

          if (response.ok) {
              showAlert('Contraseña restablecida con éxito', 'success');
          } else {
              const errorData = await response.json();
              showAlert(errorData.message || 'Error al restablecer la contraseña', 'error');
          }
      } catch (error) {
          showAlert('Error al conectar con el servidor', 'error');
      }
  });

  document.getElementById('cancel-btn').addEventListener('click', () => {
      window.location.href = 'index.html'; // Redirige a la página principal o a la página de inicio de sesión
  });

  function showAlert(message, type) {
      alertContainer.innerHTML = `
          <div class="alert ${type}">
              ${message}
          </div>
      `;
  }
});
 










