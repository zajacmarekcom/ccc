using System;
using System.ComponentModel.DataAnnotations;
using webapp.Validators;
using FluentValidation.Attributes;

namespace webapp.Models
{
    [Serializable]
    public class BusinessData
    {
        public int Id { get; set; }
        [Display(Name = "Status")]
        public int Status { get; set; }
        [Display(Name = "Rodzaj współpracy")]
        public int CooperationTypeId { get; set; }
        public DateTime AddDate { get; set; }
        [Display(Name = "Nazwa odbiorcy")]
        public string RecipientName { get; set; }
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Display(Name = "Numer budynku")]
        public string BuildingNumber { get; set; }
        [RegularExpression("^\\d{2}-\\d{3}$", ErrorMessage = "Nieprawidłowy kod pocztowy")]
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }
        [Display(Name = "Województwo")]
        public int? ProvinceId { get; set; }
        [Display(Name = "Powiat")]
        public int? DistrictId { get; set; }
        [Display(Name = "Miasto")]
        public string City { get; set; }
        [Display(Name = "Forma prawna")]
        public int? LegalFormId { get; set; }
        [Display(Name = "NIP")]
        public string NIP { get; set; }
        [Display(Name = "Numer SAP")]
        public string Sap { get; set; }
        [Display(Name = "Rok rozpoczęcia działalności")]
        public int StartYear { get; set; }
        [Display(Name = "Członek grupy")]
        public bool GroupMember { get; set; }
        [Display(Name = "Grupa")]
        public int? GroupId { get; set; }
        [Display(Name = "Właściciel/osoba zarządzająca")]
        public string Owner { get; set; }
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Telefon 2")]
        public string PhoneNumber2 { get; set; }
        [Display(Name = "Telefon 3")]
        public string PhoneNumber3 { get; set; }
        [Display(Name = "Telefon właściciela")]
        public string OwnerPhoneNumber { get; set; }
        [Display(Name = "Adres e-mail")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Nieprawidłowy adres email")]
        public string Emial { get; set; }
        [Display(Name = "Adres www")]
        public string Website { get; set; }
        [Display(Name = "Przedstawiciel handlowy")]
        public int? AgentId { get; set; }
        [Display(Name = "Osoba do kontaktu")]
        public string ContactPerson { get; set; }
        [Display(Name = "Stanowisko osoby do kontaktu")]
        public string ContactPersonPosition { get; set; }
        [Display(Name = "Adres e-mail")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Nieprawidłowy adres email")]
        public string ContactPersonEmail { get; set; }
        [Display(Name = "Telefon")]
        public string ContactPersonPhoneNumber { get; set; }
        public int BusinessId { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
        public bool IsBranch { get; set; }
    }
}