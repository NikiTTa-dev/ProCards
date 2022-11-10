function validateCardContent(value) {
    return value.length >= 0 && value.length <= 263;
}

function validateForm(form) {
    const pristine = new Pristine(form, {
        classTo: 'content-item',
        errorTextParent: 'content-item',
        errorTextTag: 'span',
        errorTextClass: 'form-error'
    }, false);

    pristine.addValidator(form.querySelector('.test'), validateCardContent, 'От 1 до 263 символов', 2, false);
    pristine.validate();
}

export { validateForm }