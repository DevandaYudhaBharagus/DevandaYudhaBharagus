﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public abstract class Karyawan
    {
        private string namaDepan;
        private string namaBelakang;
        private string socialSecurityNumber;
        public Karyawan(string Depan, string Belakang, string ssn)
        {
            namaDepan = Depan;
            namaBelakang = Belakang;
            socialSecurityNumber = ssn;
        }
        public string NamaDepan
        {
            get
            {
                return namaDepan;
            }
        }
        public string NamaBelakang
        {
            get
            {
                return namaBelakang;
            }
        }
        public string SocialSecurityNumber
        {
            get
            {
                return socialSecurityNumber;
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1}\nSocial Security Number : {2}", NamaDepan, NamaBelakang, SocialSecurityNumber);
        }
        public abstract decimal Pendapatan();
    }
    public class GajiKaryawan : Karyawan
    {
        private decimal gajiMingguan;

        public GajiKaryawan(string Depan, string Belakang, string ssn, decimal gaji)
            : base(Depan, Belakang, ssn)
        {
            GajiMingguan = gaji;
        }
        public decimal GajiMingguan
        {
            get
            {
                return gajiMingguan;
            }
            set
            {
                gajiMingguan = ((value >= 0) ? value : 0);
            }
        }
        public override decimal Pendapatan()
        {
            return GajiMingguan;
        }
        public override string ToString()
        {
            return string.Format("salaried employee: {0}\n{1}: {2:C}", base.ToString(), "weekly salary", GajiMingguan);
        }
    }
    public class KaryawanPerjam : Karyawan
    {
        private decimal upah;
        private decimal jam;

        public KaryawanPerjam(string Depan, string Belakang, string ssn, decimal upahPerjam, decimal jamKerja)
            : base(Depan, Belakang, ssn)
        {
            Upah = upahPerjam;
            Jam = jamKerja;
        }
        public decimal Upah
        {
            get
            {
                return upah;
            }
            set
            {
                upah = (value >= 0) ? value : 0;
            }
        }
        public decimal Jam
        {
            get
            {
                return jam;
            }
            set
            {
                jam = ((value >= 0) && (value <= 168)) ? value : 0;
            }
        }
        public override decimal Pendapatan()
        {
            if (Jam <= 40)
                return Upah * Jam;
            else
                return (40 * Upah) + ((Jam - 40) * 1.5M);
        }
        public override string ToString()
        {
            return string.Format("Karyawan/jam : {0}\n{1}: {2:C}; {3}: {4:F2}", base.ToString(), "Upah/jam", Upah, "Jam Kerja", Jam);
        }
    }
    public class KomisiKaryawan : Karyawan
    {
        private decimal penjualanKotor;
        private decimal tingkatKomisi;

        public KomisiKaryawan(string Depan, string Belakang, string ssn, decimal penjualan, decimal tingkat)
            : base(Depan, Belakang, ssn)
        {
            PenjualanKotor = penjualan;
            TingkatKomisi = tingkat;
        }
        public decimal PenjualanKotor
        {
            get
            {
                return penjualanKotor;
            }
            set
            {
                penjualanKotor = (value >= 0) ? value : 0;
            }
        }
        public decimal TingkatKomisi
        {
            get
            {
                return tingkatKomisi;
            }
            set
            {
                tingkatKomisi = (value > 0 && value < 1) ? value : 0;
            }
        }
        public override decimal Pendapatan()
        {
            return TingkatKomisi * PenjualanKotor;
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}\n{2}: {3:C}\n{4}: {5:F2}", "Komisi Karyawan", base.ToString(), "Penjualan Kotor", PenjualanKotor, "Tingkat Komisi", TingkatKomisi);
        }
    }
    public class KomisiTambahanKaryawan : KomisiKaryawan
    {
        private decimal gajiPokok;

        public KomisiTambahanKaryawan(string Depan, string Belakang, string ssn, decimal penjualan, decimal tingkat, decimal gaji)
            : base(Depan, Belakang, ssn, penjualan, tingkat)
        {
            GajiPokok = gaji;
        }
        public decimal GajiPokok
        {
            get
            {
                return gajiPokok;
            }
            set
            {
                gajiPokok = (value >= 0) ? value : 0;
            }
        }
        public override decimal Pendapatan()
        {
            return GajiPokok * base.Pendapatan();
        }
        public override string ToString()
        {
            return string.Format("{0} {1}; {2}: {3:C}", "gaji-pokok", base.ToString(), "gaji pokok", GajiPokok);
        }
    }
    public class PayrollSystemTest
    {
        static void Main(string[] args)
        {
            GajiKaryawan gajiKaryawan = new GajiKaryawan("John", "Smith", "111-11-1111", 800.00M);
            KaryawanPerjam karyawanPerjam = new KaryawanPerjam("Karen", "Price", "222-22-2222", 16.75M, 40.0M);
            KomisiKaryawan komisiKaryawan = new KomisiKaryawan("Sue", "Jones", "333-33-3333", 10000.00M, .06M);
            KomisiTambahanKaryawan komisiTambahanKaryawan = new KomisiTambahanKaryawan("Bob", "Lewis", "444-44-4444", 5000.00M, .04M, 300.00M);

            Console.WriteLine("Karyawan diproses secara individual :\n");
            Console.WriteLine("{0}\n{1}: {2:C}\n", gajiKaryawan, "Diperoleh", gajiKaryawan.Pendapatan());
            Console.WriteLine("{0}\n{1}: {2:C}\n", karyawanPerjam, "Diperoleh", karyawanPerjam.Pendapatan());
            Console.WriteLine("{0}\n{1}: {2:C}\n", komisiKaryawan, "Diperoleh", komisiKaryawan.Pendapatan());
            Console.WriteLine("{0}\n{1}: {2:C}\n", komisiTambahanKaryawan, "Diperoleh", komisiTambahanKaryawan.Pendapatan());

            Karyawan[] karyawan = new Karyawan[4];

            karyawan[0] = gajiKaryawan;
            karyawan[1] = karyawanPerjam;
            karyawan[2] = komisiKaryawan;
            karyawan[3] = komisiTambahanKaryawan;

            Console.WriteLine("Karyawan diproses secara Polymorphically:\n");

            foreach (Karyawan karyawanSekarang in karyawan)
            {
                Console.WriteLine(karyawanSekarang);
                if (karyawanSekarang is KomisiTambahanKaryawan)
                {
                    KomisiTambahanKaryawan pegawai = (KomisiTambahanKaryawan)karyawanSekarang;

                    pegawai.GajiPokok *= 1.10M;
                    Console.WriteLine("Gaji pokok baru dengan kenaikan 10% adalah : {0:C}", pegawai.GajiPokok);
                }
                Console.WriteLine("Diperoleh {0:C}\n", karyawanSekarang.Pendapatan());
            }
            for (int j = 0; j < karyawan.Length; j++)
                Console.WriteLine("Employee {0} is a {1}", j, karyawan[j].GetType());
            Console.ReadLine();
        }
    }
}