﻿using System;
using System.Text.Json.Serialization;

namespace WebSchool.Models
{
    public class PelajaranInputModel
    {
        [JsonPropertyName("namaPelajaran")]
        public string NamaPelajaran { get; set; }
        [JsonPropertyName("deskripsi")]
        public string Deskripsi { get; set; }
        [JsonPropertyName("aktif")]
        public bool? Aktif { get; set; }
        [JsonPropertyName("terminalBuat")]
        public string TerminalBuat { get; set; }
    }
}
