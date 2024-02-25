﻿using System;

namespace Backend_School.WebApi.Models.Request
{
    public class DaftarUlangAddRequest
    {
        public long IdSiswa { get; set; }
        public long IdTahunAjaran { get; set; }
        public long IdTahunAjaranTo { get; set; }
        public long IdDariKelas { get; set; }
        public long DariKelas { get; set; }
        public long IdKeKelas { get; set; }
        public long KeKelas { get; set; }
        public string NamaPembayaran { get; set; }
        public long NilaiDaftarUlang { get; set; }
        public long Bayar { get; set; }
        public long Kembalian { get; set; }
        public string Deskripsi { get; set; }
        public string PenggunaBuat { get; set; }
        public string TerminalBuat { get; set; }
        public DateTime? TanggalBuat { get; set; }
    }
}
