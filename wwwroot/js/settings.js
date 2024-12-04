$(document).ready(function () {
    const avatarFormControlInput = document.getElementById('avatar-form-control');
    const updateAvatarSubmitButton = document.getElementById('update-avatar-submit');

    avatarFormControlInput.onchange = () => updateAvatarSubmitButton.hidden = false;
})