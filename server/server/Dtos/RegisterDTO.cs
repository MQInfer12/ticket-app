﻿using System.Text.Json.Serialization;

namespace server.Dtos
{
    public class RegisterDTO
    {
        public string Ci { get; set; } = "";
        public string Nombres { get; set; } = "";
        public string Appaterno { get; set; } = "";
        public string Apmaterno { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Contrasenia { get; set; } = "";
        public string ContraseniaRepit { get; set; } = "";

        //IMG
        public IFormFile Image { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? ImagePath { get; set; }

    }
}
