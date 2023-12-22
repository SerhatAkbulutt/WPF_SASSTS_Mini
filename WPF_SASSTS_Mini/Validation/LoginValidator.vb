Imports FluentValidation

Public Class LoginValidator
    Inherits AbstractValidator(Of Login)
    Public Sub New()
        RuleFor(Function(login) login.Username) _
            .NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz.") _
            .Length(5, 50).WithMessage("Kullanıcı Adı en az 5, en fazla 50 karakter olmalıdır.")
        RuleFor(Function(login) login.Password) _
            .NotEmpty().WithMessage("Parola Boş Olamaz.") _
            .Length(5, 50).WithMessage("Parola en az 5, en fazla 50 karakter olmalıdır.")
    End Sub
End Class
