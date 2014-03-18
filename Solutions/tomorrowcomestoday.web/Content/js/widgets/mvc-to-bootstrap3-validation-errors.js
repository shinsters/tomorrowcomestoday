$(function () {
    $('.input-validation-valid, .input-validation-error').each(function () {
        $(this).addClass('help-inline');
    });

    $('.validation-summary-errors').each(function () {
        $(this).addClass('alert');
        $(this).addClass('alert-error');
        $(this).addClass('alert-block');
    });
    
    $('form').each(function () {
        $(this).find('.form-group').each(function () {
            if ($(this).find('.input-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });
});