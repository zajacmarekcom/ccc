using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webapp.Enums;
using webapp.Models;

namespace webapp.ViewModels
{
    public class ClientDetailsViewModel
    {
        public Business Business { get; set; }
        public ClientStatus Status { get; set; }

        [Display(Name = "Rodzaj współpracy")]
        public string CooperationType { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime AddDate { get; set; }

        [Display(Name = "Nazwa odbiorcy")]
        public string Name { get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Numer budynku")]
        public string BuildingNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        [Display(Name = "Województwo")]
        public string Province { get; set; }

        [Display(Name = "Powiat")]
        public string District { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "NIP")]
        public string Nip { get; set; }

        [Display(Name = "Forma prawna")]
        public string LegalForm { get; set; }

        [Display(Name = "Rok rozpoczęcia działalności")]
        public string StartYear { get; set; }

        [Display(Name = "Członek grupy")]
        public bool GroupMember { get; set; }

        [Display(Name = "Grupa")]
        public string Group { get; set; }

        [Display(Name = "Właściciel")]
        public string Owner { get; set; }

        [Display(Name = "Telefon właściciela")]
        public string OwnerPhone { get; set; }

        [Display(Name = "Telefon 1")]
        public string Phone1 { get; set; }

        [Display(Name = "Telefon 2")]
        public string Phone2 { get; set; }

        [Display(Name = "Telefon 3")]
        public string Phone3 { get; set; }

        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Adres www")]
        public string Website { get; set; }

        [Display(Name = "Przedstawiciel handlowy")]
        public string Agent { get; set; }

        [Display(Name = "Osoba do kontaktu")]
        public string ContactPerson { get; set; }

        [Display(Name = "Stanowisko osoby do kontaktu")]
        public string ContactPersonPosition { get; set; }

        [Display(Name = "Adres e-mail")]
        public string ContactPersonEmail { get; set; }

        [Display(Name = "Telefon")]
        public string ContactPersonPhone { get; set; }
        public IEnumerable<Visit> Visits { get; set; }
    }
}
