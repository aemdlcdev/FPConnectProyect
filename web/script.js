document.addEventListener('DOMContentLoaded', function() {
  const form = document.getElementById('password-form');
  const currentPasswordInput = document.getElementById('current-password');
  const newPasswordInput = document.getElementById('new-password');
  const confirmPasswordInput = document.getElementById('confirm-password');
  const submitBtn = document.getElementById('submit-btn');
  const cancelBtn = document.getElementById('cancel-btn');
  const alertContainer = document.getElementById('alert-container');
  
  // Show alert message
  function showAlert(message, type) {
    alertContainer.innerHTML = '';
    
    const alert = document.createElement('div');
    alert.className = `alert ${type === 'error' ? 'alert-error' : 'alert-success'}`;
    
    const iconName = type === 'error' ? '⚠️' : '✅';
    
    alert.innerHTML = `
      <span class="alert-icon">${iconName}</span>
      <span>${message}</span>
    `;
    
    alertContainer.appendChild(alert);
  }
  
  // Clear form
  function clearForm() {
    form.reset();
    alertContainer.innerHTML = '';
  }
  
  // Handle form submission
  form.addEventListener('submit', function(e) {
    e.preventDefault();
    
    // Clear previous alerts
    alertContainer.innerHTML = '';
    
    const currentPassword = currentPasswordInput.value;
    const newPassword = newPasswordInput.value;
    const confirmPassword = confirmPasswordInput.value;
    
    // Validate passwords
    if (newPassword !== confirmPassword) {
      showAlert('New passwords don\'t match', 'error');
      return;
    }
    
    if (newPassword.length < 8) {
      showAlert('Password must be at least 8 characters', 'error');
      return;
    }
    
    // Disable button and show loading state
    submitBtn.disabled = true;
    submitBtn.textContent = 'Changing...';
    
    // Simulate API call
    setTimeout(function() {
      // Here you would make an actual API call to change the password
      
      // Reset form and show success
      clearForm();
      showAlert('Password changed successfully!', 'success');
      
      // Reset button
      submitBtn.disabled = false;
      submitBtn.textContent = 'Change Password';
    }, 1000);
  });
  
  // Handle cancel button
  cancelBtn.addEventListener('click', clearForm);
});