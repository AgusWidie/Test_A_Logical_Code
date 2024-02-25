﻿using System.Text.Json.Serialization;

namespace WebSchool.Models
{
    public class CheckJadwalPiketGuruInputModel
    {
        [JsonPropertyName("idTahunAjaran")]
        public long IdTahunAjaran { get; set; }
        [JsonPropertyName("idGuru")]
        public long IdGuru { get; set; }
        [JsonPropertyName("hari")]
        public string Hari { get; set; }
    }
}
