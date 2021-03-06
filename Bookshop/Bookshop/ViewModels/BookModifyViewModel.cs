﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bookshop.ViewModels
{
    public class BookModifyViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł musi mieć od 2 do 50 znaków")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Data musi być zapisana w formacie zaproponowanym przez kalendarz")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data wydania")]
        [Range(typeof(DateTime), "1/1/1700", "1/1/2050",
            ErrorMessage = "Poprawna data to data między {1} i {2}")]
        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage = "ISBN musi mieć od 10 do 17 znaków")]
        [StringLength(17, MinimumLength = 10)]
        public string ISBN { get; set; }

        [Display(Name = "Opis")]
        [StringLength(2000, ErrorMessage = "Maksymalna dłuość opisu to 2000 znaków")]
        public string Description { get; set; }

        [Display(Name = "Obrazek")]
        public string ImagePath { get; set; }

        [Display(Name = "Autor")]
        public int AuthorId { get; set; }
        public List<SelectListItem> Authors { get; set; }
    }
}