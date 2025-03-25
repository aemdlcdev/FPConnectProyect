document.addEventListener('DOMContentLoaded', () => {
  const form = document.getElementById('password-form');
  const alertContainer = document.getElementById('alert-container');
  const urlParams = new URLSearchParams(window.location.search);
  const token = urlParams.get('token');
  const newPasswordInput = document.getElementById('new-password');
  const confirmPasswordInput = document.getElementById('confirm-password');

  // Crear contenedor para las reglas de contraseña
  const passwordRules = document.createElement('div');
  passwordRules.className = 'password-rules';
  passwordRules.innerHTML = `
    <div class="rule" id="length-rule">
      <span class="rule-icon">❌</span>
      <span class="rule-text">Al menos 8 caracteres</span>
    </div>
    <div class="rule" id="uppercase-rule">
      <span class="rule-icon">❌</span>
      <span class="rule-text">Al menos una mayúscula</span>
    </div>
    <div class="rule" id="lowercase-rule">
      <span class="rule-icon">❌</span>
      <span class="rule-text">Al menos una minúscula</span>
    </div>
    <div class="rule" id="number-rule">
      <span class="rule-icon">❌</span>
      <span class="rule-text">Al menos un número</span>
    </div>
    <div class="rule" id="special-rule">
      <span class="rule-icon">❌</span>
      <span class="rule-text">Al menos un carácter especial (!@#$%^&*)</span>
    </div>
  `;
  newPasswordInput.parentNode.insertBefore(passwordRules, newPasswordInput.nextSibling);

  // Función para validar contraseña
  function validatePassword(password) {
    const rules = {
      length: password.length >= 8,
      uppercase: /[A-Z]/.test(password),
      lowercase: /[a-z]/.test(password),
      number: /[0-9]/.test(password),
      special: /[!@#$%^&*]/.test(password)
    };

    // Actualizar iconos y estilos
    Object.keys(rules).forEach(rule => {
      const ruleElement = document.getElementById(`${rule}-rule`);
      const icon = ruleElement.querySelector('.rule-icon');
      const text = ruleElement.querySelector('.rule-text');
      
      if (rules[rule]) {
        icon.textContent = '✅';
        icon.style.color = '#2ecc71';
        text.style.color = '#2ecc71';
      } else {
        icon.textContent = '❌';
        icon.style.color = '#e74c3c';
        text.style.color = '#7f8c8d';
      }
    });

    return Object.values(rules).every(Boolean);
  }

  // Validación en tiempo real
  newPasswordInput.addEventListener('input', () => {
    validatePassword(newPasswordInput.value);
  });

  form.addEventListener('submit', async (event) => {
      event.preventDefault();

      const newPassword = newPasswordInput.value;
      const confirmPassword = confirmPasswordInput.value;

      // Validar todas las reglas antes de enviar
      if (!validatePassword(newPassword)) {
          showAlert('Por favor, asegúrate de que tu contraseña cumpla con todos los requisitos de seguridad', 'error');
          return;
      }

      if (newPassword !== confirmPassword) {
          showAlert('Las contraseñas no coinciden. Por favor, verifica e intenta nuevamente', 'error');
          return;
      }

      try {
          const response = await fetch('http://localhost:5223/api/user/confirm-reset-password', {
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
              showAlert('¡Contraseña actualizada con éxito! Serás redirigido al inicio de sesión', 'success');
              setTimeout(() => {
                  window.location.href = 'index.html';
              }, 2000);
          } else {
              const errorData = await response.json();
              showAlert(errorData.message || 'Ha ocurrido un error al actualizar tu contraseña. Por favor, intenta nuevamente', 'error');
          }
      } catch (error) {
          showAlert('No se pudo conectar con el servidor. Por favor, verifica tu conexión e intenta nuevamente', 'error');
      }
  });

  document.getElementById('cancel-btn').addEventListener('click', () => {
      window.location.href = 'index.html'; 
  });

  function showAlert(message, type) {
      // Limpiar alertas anteriores
      alertContainer.innerHTML = '';
      
      // Crear nueva alerta
      const alert = document.createElement('div');
      alert.className = `alert alert-${type}`;
      alert.textContent = message;
      
      // Agregar al contenedor
      alertContainer.appendChild(alert);
      
      // Remover la alerta después de 5 segundos si es de éxito
      if (type === 'success') {
          setTimeout(() => {
              alert.style.opacity = '0';
              alert.style.transform = 'translateY(-10px)';
              setTimeout(() => alert.remove(), 300);
          }, 5000);
      }
  }
});
 










