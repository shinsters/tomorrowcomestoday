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
        $(this).find('div.form-group').each(function () {
            if ($(this).find('.input-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });

    $("input[type='password'], input[type='text']").blur(function () {
        if ($(this).hasClass('input-validation-error') == true || $(this).closest(".form-group").find('.input-validation-error').length > 0) {
            $(this).addClass('has-error');
            $(this).closest(".form-group").addClass("has-error");
        } else {
            $(this).removeClass('has-error');
            $(this).closest(".form-group").removeClass("has-error");
        }
    });
});

