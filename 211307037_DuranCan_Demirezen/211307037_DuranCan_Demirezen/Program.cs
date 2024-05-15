using System;
using System.Collections.Generic;
using System.Text;

namespace _211307037_DuranCan_Demirezen
{
    using System;

    class Arac
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public int UretimYili { get; set; }
        public double GunlukKiraUcreti { get; set; }
        public bool KiralanabilirlikDurumu { get; set; }

        public virtual double KiraUcretiHesapla()
        {
            return GunlukKiraUcreti;
        }
    }

    class Otomobil : Arac
    {
        public string VitesTipi { get; set; }
        public double YakitTuketimi { get; set; }

        public override double KiraUcretiHesapla()
        {
            return GunlukKiraUcreti * 1.20; // %20 artırım
        }
    }

    class Kamyonet : Arac
    {
        public double YukTasimaKapasitesi { get; set; }

        public override double KiraUcretiHesapla()
        {
            return GunlukKiraUcreti * 1.50; // %50 artırım
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Otomobil otomobil = new Otomobil();
            Console.WriteLine("Otomobil için bilgileri girin:");
            Console.Write("Marka: ");
            otomobil.Marka = Console.ReadLine();
            Console.Write("Model: ");
            otomobil.Model = Console.ReadLine();
            Console.Write("Üretim yılı: ");
            otomobil.UretimYili = int.Parse(Console.ReadLine());
            Console.Write("Günlük kira ücreti: ");
            otomobil.GunlukKiraUcreti = double.Parse(Console.ReadLine());
            Console.Write("Kiralanabilirlik durumu (true/false): ");
            otomobil.KiralanabilirlikDurumu = bool.Parse(Console.ReadLine());
            Console.Write("Vites tipi: ");
            otomobil.VitesTipi = Console.ReadLine();
            Console.Write("Yakıt tüketimi: ");
            otomobil.YakitTuketimi = double.Parse(Console.ReadLine());

            Kamyonet kamyonet = new Kamyonet();
            Console.WriteLine("\nKamyonet için bilgileri girin:");
            Console.Write("Marka: ");
            kamyonet.Marka = Console.ReadLine();
            Console.Write("Model: ");
            kamyonet.Model = Console.ReadLine();
            Console.Write("Üretim yılı: ");
            kamyonet.UretimYili = int.Parse(Console.ReadLine());
            Console.Write("Günlük kira ücreti: ");
            kamyonet.GunlukKiraUcreti = double.Parse(Console.ReadLine());
            Console.Write("Kiralanabilirlik durumu (true/false): ");
            kamyonet.KiralanabilirlikDurumu = bool.Parse(Console.ReadLine());
            Console.Write("Yük taşıma kapasitesi: ");
            kamyonet.YukTasimaKapasitesi = double.Parse(Console.ReadLine());

            Console.WriteLine("\nOtomobil kira ücreti: " + otomobil.KiraUcretiHesapla());
            Console.WriteLine("Kamyonet kira ücreti: " + kamyonet.KiraUcretiHesapla());
            Console.Read();
        }
    }

}
