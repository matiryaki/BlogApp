using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObject
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
       
          public string UserName { get; set; }
        [DisplayName("E-Posta"), Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."),EmailAddress(ErrorMessage ="{0} geçerli bir E-Posta adresi giriniz!")]

        public string Email { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."),Compare("Password",ErrorMessage ="{0} ile {1} uyuşmuyor")]

        public string Password { get; set; }
        [DisplayName("Şifre Tekrarı"), Required(ErrorMessage = "{0} alanı boş geçilemez!"), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."), Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor")]
        public string RePassword { get; set; }
    }
}