using Microsoft.AspNetCore.Identity;
using System.Resources;

namespace UrbanDictionary1
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordMismatch()
        {            
            var error = base.PasswordMismatch();
            error.Description = "mue";
            return error;
           
        }

        public override IdentityError DuplicateEmail(string email)
        {
           
            var error = base.DuplicateEmail(email);
            error.Description = "Emailul este deja folosit";
            return error;
           
        }

        public override IdentityError PasswordRequiresLower()
        {

            var error = base.PasswordRequiresLower();
            error.Description = "Parola trebuie sa contina cel putin o litera mica";
            return error;
        }

        public override IdentityError PasswordTooShort(int length)
        {

            var error = base.PasswordTooShort(length);
            error.Description = "Parola trebuie sa contina minim " + length + " caractere";
            return error;
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {

            var error = base.PasswordRequiresNonAlphanumeric();
            error.Description = "Parola trebuie sa contina cel putin un caracter non-alfanumeric";
            return error;

        }

        public override IdentityError PasswordRequiresDigit()
        {

            var error = base.PasswordRequiresDigit();
            error.Description = "Parola trebuie sa contina cel putin o cifra";
            return error;

        }
        public override IdentityError PasswordRequiresUpper()
        {
            var error = base.PasswordRequiresUpper();
            error.Description = "Parola trebuie sa contina cel putin o majuscula";
            return error;
        }

    }
}
