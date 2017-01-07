using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TIPicLib
{
    public sealed class TIPicFile
    {
        enum TICalcType { TI73, TI82, TI83, TI83F, TI85, TI86, TI89, TI92, TI92P}

        public class TIPictureFormat
        {
            public short Size;                      //2 bytes
            public BitArray Data;                   //1008 bytes
        }

        public class TIVariableFormat
        {
            public short Signiture;                 //2 bytes
            public short Length;                    //2 bytes
            public byte ID;                         //1 byte

            public TIPictureFormat Data;            //n bytes
        }

        public class TI7x8xVariableFormat : TIVariableFormat
        {
            public byte[] Name;                     //8 bytes
            public short LengthCopy;                //2 bytes
        }

        public class TI8xpVariableFormat : TI7x8xVariableFormat
        {
            public byte Version;                    //1 byte
            public byte Flag;                       //1 byte
        }

        public class TI85VariableFormat : TIVariableFormat
        {
            public byte Version;                    //1 byte
            public byte Flag;                       //1 byte
        }

        public struct TIFileFormat
        {
            public byte[] PrimarySigniture;         //8 bytes
            public byte[] SecndarySigniture;        //3 bytes
            public byte[] Comment;                  //42 bytes
            public short DataLength;                //2 bytes
            public TIVariableFormat[] Variables;    //n bytes
            public BitArray Checksum;               //2 bytes
        }

        TIFileFormat file;
        TICalcType type;

        public TIPicFile(Stream input)
        {
            BinaryReader reader = new BinaryReader(input);
            file.PrimarySigniture = reader.ReadBytes(8);
            string pSig = Encoding.ASCII.GetString(file.PrimarySigniture);
            MessageBox.Show(pSig);
            if (pSig.Equals("**TI73**"))
            {
                type = TICalcType.TI73;
            }
            else if (pSig.Equals("**TI82**"))
            {
                type = TICalcType.TI82;
            }
            else if (pSig.Equals("**TI83**"))
            {
                type = TICalcType.TI83;
            }
            else if (pSig.Equals("**TI83F*"))
            {
                type = TICalcType.TI83F;
            }
            else if (pSig.Equals("**TI85**"))
            {
                type = TICalcType.TI85;
            }
            else if (pSig.Equals("**TI86**"))
            {
                type = TICalcType.TI86;
            }
            else if (pSig.Equals("**TI89**"))
            {
                type = TICalcType.TI89;
            }
            else if (pSig.Equals("**TI92**"))
            {
                type = TICalcType.TI92;
            }
            else if (pSig.Equals("**TI92P*"))
            {
                type = TICalcType.TI92P;
            }
            else
            {
                throw new Exception("Invalid TI Calculator Picture Format");
            }
        }

        /// <summary>
        /// Converts the TIPicFile to a System.Drawing.Bitmap
        /// </summary>
        /// <returns>A Bitmap</returns>
        //public Bitmap GetBitmap()
        //{

        //}
    }
}
