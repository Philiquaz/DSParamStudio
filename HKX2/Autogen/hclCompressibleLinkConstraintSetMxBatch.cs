using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hclCompressibleLinkConstraintSetMxBatch : IHavokObject
    {
        public virtual uint Signature { get => 3631361145; }
        
        public float m_restLengths_0;
        public float m_restLengths_1;
        public float m_restLengths_2;
        public float m_restLengths_3;
        public float m_restLengths_4;
        public float m_restLengths_5;
        public float m_restLengths_6;
        public float m_restLengths_7;
        public float m_restLengths_8;
        public float m_restLengths_9;
        public float m_restLengths_10;
        public float m_restLengths_11;
        public float m_restLengths_12;
        public float m_restLengths_13;
        public float m_restLengths_14;
        public float m_restLengths_15;
        public float m_compressionLengths_0;
        public float m_compressionLengths_1;
        public float m_compressionLengths_2;
        public float m_compressionLengths_3;
        public float m_compressionLengths_4;
        public float m_compressionLengths_5;
        public float m_compressionLengths_6;
        public float m_compressionLengths_7;
        public float m_compressionLengths_8;
        public float m_compressionLengths_9;
        public float m_compressionLengths_10;
        public float m_compressionLengths_11;
        public float m_compressionLengths_12;
        public float m_compressionLengths_13;
        public float m_compressionLengths_14;
        public float m_compressionLengths_15;
        public float m_stiffnessesA_0;
        public float m_stiffnessesA_1;
        public float m_stiffnessesA_2;
        public float m_stiffnessesA_3;
        public float m_stiffnessesA_4;
        public float m_stiffnessesA_5;
        public float m_stiffnessesA_6;
        public float m_stiffnessesA_7;
        public float m_stiffnessesA_8;
        public float m_stiffnessesA_9;
        public float m_stiffnessesA_10;
        public float m_stiffnessesA_11;
        public float m_stiffnessesA_12;
        public float m_stiffnessesA_13;
        public float m_stiffnessesA_14;
        public float m_stiffnessesA_15;
        public float m_stiffnessesB_0;
        public float m_stiffnessesB_1;
        public float m_stiffnessesB_2;
        public float m_stiffnessesB_3;
        public float m_stiffnessesB_4;
        public float m_stiffnessesB_5;
        public float m_stiffnessesB_6;
        public float m_stiffnessesB_7;
        public float m_stiffnessesB_8;
        public float m_stiffnessesB_9;
        public float m_stiffnessesB_10;
        public float m_stiffnessesB_11;
        public float m_stiffnessesB_12;
        public float m_stiffnessesB_13;
        public float m_stiffnessesB_14;
        public float m_stiffnessesB_15;
        public ushort m_particlesA_0;
        public ushort m_particlesA_1;
        public ushort m_particlesA_2;
        public ushort m_particlesA_3;
        public ushort m_particlesA_4;
        public ushort m_particlesA_5;
        public ushort m_particlesA_6;
        public ushort m_particlesA_7;
        public ushort m_particlesA_8;
        public ushort m_particlesA_9;
        public ushort m_particlesA_10;
        public ushort m_particlesA_11;
        public ushort m_particlesA_12;
        public ushort m_particlesA_13;
        public ushort m_particlesA_14;
        public ushort m_particlesA_15;
        public ushort m_particlesB_0;
        public ushort m_particlesB_1;
        public ushort m_particlesB_2;
        public ushort m_particlesB_3;
        public ushort m_particlesB_4;
        public ushort m_particlesB_5;
        public ushort m_particlesB_6;
        public ushort m_particlesB_7;
        public ushort m_particlesB_8;
        public ushort m_particlesB_9;
        public ushort m_particlesB_10;
        public ushort m_particlesB_11;
        public ushort m_particlesB_12;
        public ushort m_particlesB_13;
        public ushort m_particlesB_14;
        public ushort m_particlesB_15;
        
        public virtual void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            m_restLengths_0 = br.ReadSingle();
            m_restLengths_1 = br.ReadSingle();
            m_restLengths_2 = br.ReadSingle();
            m_restLengths_3 = br.ReadSingle();
            m_restLengths_4 = br.ReadSingle();
            m_restLengths_5 = br.ReadSingle();
            m_restLengths_6 = br.ReadSingle();
            m_restLengths_7 = br.ReadSingle();
            m_restLengths_8 = br.ReadSingle();
            m_restLengths_9 = br.ReadSingle();
            m_restLengths_10 = br.ReadSingle();
            m_restLengths_11 = br.ReadSingle();
            m_restLengths_12 = br.ReadSingle();
            m_restLengths_13 = br.ReadSingle();
            m_restLengths_14 = br.ReadSingle();
            m_restLengths_15 = br.ReadSingle();
            m_compressionLengths_0 = br.ReadSingle();
            m_compressionLengths_1 = br.ReadSingle();
            m_compressionLengths_2 = br.ReadSingle();
            m_compressionLengths_3 = br.ReadSingle();
            m_compressionLengths_4 = br.ReadSingle();
            m_compressionLengths_5 = br.ReadSingle();
            m_compressionLengths_6 = br.ReadSingle();
            m_compressionLengths_7 = br.ReadSingle();
            m_compressionLengths_8 = br.ReadSingle();
            m_compressionLengths_9 = br.ReadSingle();
            m_compressionLengths_10 = br.ReadSingle();
            m_compressionLengths_11 = br.ReadSingle();
            m_compressionLengths_12 = br.ReadSingle();
            m_compressionLengths_13 = br.ReadSingle();
            m_compressionLengths_14 = br.ReadSingle();
            m_compressionLengths_15 = br.ReadSingle();
            m_stiffnessesA_0 = br.ReadSingle();
            m_stiffnessesA_1 = br.ReadSingle();
            m_stiffnessesA_2 = br.ReadSingle();
            m_stiffnessesA_3 = br.ReadSingle();
            m_stiffnessesA_4 = br.ReadSingle();
            m_stiffnessesA_5 = br.ReadSingle();
            m_stiffnessesA_6 = br.ReadSingle();
            m_stiffnessesA_7 = br.ReadSingle();
            m_stiffnessesA_8 = br.ReadSingle();
            m_stiffnessesA_9 = br.ReadSingle();
            m_stiffnessesA_10 = br.ReadSingle();
            m_stiffnessesA_11 = br.ReadSingle();
            m_stiffnessesA_12 = br.ReadSingle();
            m_stiffnessesA_13 = br.ReadSingle();
            m_stiffnessesA_14 = br.ReadSingle();
            m_stiffnessesA_15 = br.ReadSingle();
            m_stiffnessesB_0 = br.ReadSingle();
            m_stiffnessesB_1 = br.ReadSingle();
            m_stiffnessesB_2 = br.ReadSingle();
            m_stiffnessesB_3 = br.ReadSingle();
            m_stiffnessesB_4 = br.ReadSingle();
            m_stiffnessesB_5 = br.ReadSingle();
            m_stiffnessesB_6 = br.ReadSingle();
            m_stiffnessesB_7 = br.ReadSingle();
            m_stiffnessesB_8 = br.ReadSingle();
            m_stiffnessesB_9 = br.ReadSingle();
            m_stiffnessesB_10 = br.ReadSingle();
            m_stiffnessesB_11 = br.ReadSingle();
            m_stiffnessesB_12 = br.ReadSingle();
            m_stiffnessesB_13 = br.ReadSingle();
            m_stiffnessesB_14 = br.ReadSingle();
            m_stiffnessesB_15 = br.ReadSingle();
            m_particlesA_0 = br.ReadUInt16();
            m_particlesA_1 = br.ReadUInt16();
            m_particlesA_2 = br.ReadUInt16();
            m_particlesA_3 = br.ReadUInt16();
            m_particlesA_4 = br.ReadUInt16();
            m_particlesA_5 = br.ReadUInt16();
            m_particlesA_6 = br.ReadUInt16();
            m_particlesA_7 = br.ReadUInt16();
            m_particlesA_8 = br.ReadUInt16();
            m_particlesA_9 = br.ReadUInt16();
            m_particlesA_10 = br.ReadUInt16();
            m_particlesA_11 = br.ReadUInt16();
            m_particlesA_12 = br.ReadUInt16();
            m_particlesA_13 = br.ReadUInt16();
            m_particlesA_14 = br.ReadUInt16();
            m_particlesA_15 = br.ReadUInt16();
            m_particlesB_0 = br.ReadUInt16();
            m_particlesB_1 = br.ReadUInt16();
            m_particlesB_2 = br.ReadUInt16();
            m_particlesB_3 = br.ReadUInt16();
            m_particlesB_4 = br.ReadUInt16();
            m_particlesB_5 = br.ReadUInt16();
            m_particlesB_6 = br.ReadUInt16();
            m_particlesB_7 = br.ReadUInt16();
            m_particlesB_8 = br.ReadUInt16();
            m_particlesB_9 = br.ReadUInt16();
            m_particlesB_10 = br.ReadUInt16();
            m_particlesB_11 = br.ReadUInt16();
            m_particlesB_12 = br.ReadUInt16();
            m_particlesB_13 = br.ReadUInt16();
            m_particlesB_14 = br.ReadUInt16();
            m_particlesB_15 = br.ReadUInt16();
        }
        
        public virtual void Write(PackFileSerializer s, BinaryWriterEx bw)
        {
            bw.WriteSingle(m_restLengths_0);
            bw.WriteSingle(m_restLengths_1);
            bw.WriteSingle(m_restLengths_2);
            bw.WriteSingle(m_restLengths_3);
            bw.WriteSingle(m_restLengths_4);
            bw.WriteSingle(m_restLengths_5);
            bw.WriteSingle(m_restLengths_6);
            bw.WriteSingle(m_restLengths_7);
            bw.WriteSingle(m_restLengths_8);
            bw.WriteSingle(m_restLengths_9);
            bw.WriteSingle(m_restLengths_10);
            bw.WriteSingle(m_restLengths_11);
            bw.WriteSingle(m_restLengths_12);
            bw.WriteSingle(m_restLengths_13);
            bw.WriteSingle(m_restLengths_14);
            bw.WriteSingle(m_restLengths_15);
            bw.WriteSingle(m_compressionLengths_0);
            bw.WriteSingle(m_compressionLengths_1);
            bw.WriteSingle(m_compressionLengths_2);
            bw.WriteSingle(m_compressionLengths_3);
            bw.WriteSingle(m_compressionLengths_4);
            bw.WriteSingle(m_compressionLengths_5);
            bw.WriteSingle(m_compressionLengths_6);
            bw.WriteSingle(m_compressionLengths_7);
            bw.WriteSingle(m_compressionLengths_8);
            bw.WriteSingle(m_compressionLengths_9);
            bw.WriteSingle(m_compressionLengths_10);
            bw.WriteSingle(m_compressionLengths_11);
            bw.WriteSingle(m_compressionLengths_12);
            bw.WriteSingle(m_compressionLengths_13);
            bw.WriteSingle(m_compressionLengths_14);
            bw.WriteSingle(m_compressionLengths_15);
            bw.WriteSingle(m_stiffnessesA_0);
            bw.WriteSingle(m_stiffnessesA_1);
            bw.WriteSingle(m_stiffnessesA_2);
            bw.WriteSingle(m_stiffnessesA_3);
            bw.WriteSingle(m_stiffnessesA_4);
            bw.WriteSingle(m_stiffnessesA_5);
            bw.WriteSingle(m_stiffnessesA_6);
            bw.WriteSingle(m_stiffnessesA_7);
            bw.WriteSingle(m_stiffnessesA_8);
            bw.WriteSingle(m_stiffnessesA_9);
            bw.WriteSingle(m_stiffnessesA_10);
            bw.WriteSingle(m_stiffnessesA_11);
            bw.WriteSingle(m_stiffnessesA_12);
            bw.WriteSingle(m_stiffnessesA_13);
            bw.WriteSingle(m_stiffnessesA_14);
            bw.WriteSingle(m_stiffnessesA_15);
            bw.WriteSingle(m_stiffnessesB_0);
            bw.WriteSingle(m_stiffnessesB_1);
            bw.WriteSingle(m_stiffnessesB_2);
            bw.WriteSingle(m_stiffnessesB_3);
            bw.WriteSingle(m_stiffnessesB_4);
            bw.WriteSingle(m_stiffnessesB_5);
            bw.WriteSingle(m_stiffnessesB_6);
            bw.WriteSingle(m_stiffnessesB_7);
            bw.WriteSingle(m_stiffnessesB_8);
            bw.WriteSingle(m_stiffnessesB_9);
            bw.WriteSingle(m_stiffnessesB_10);
            bw.WriteSingle(m_stiffnessesB_11);
            bw.WriteSingle(m_stiffnessesB_12);
            bw.WriteSingle(m_stiffnessesB_13);
            bw.WriteSingle(m_stiffnessesB_14);
            bw.WriteSingle(m_stiffnessesB_15);
            bw.WriteUInt16(m_particlesA_0);
            bw.WriteUInt16(m_particlesA_1);
            bw.WriteUInt16(m_particlesA_2);
            bw.WriteUInt16(m_particlesA_3);
            bw.WriteUInt16(m_particlesA_4);
            bw.WriteUInt16(m_particlesA_5);
            bw.WriteUInt16(m_particlesA_6);
            bw.WriteUInt16(m_particlesA_7);
            bw.WriteUInt16(m_particlesA_8);
            bw.WriteUInt16(m_particlesA_9);
            bw.WriteUInt16(m_particlesA_10);
            bw.WriteUInt16(m_particlesA_11);
            bw.WriteUInt16(m_particlesA_12);
            bw.WriteUInt16(m_particlesA_13);
            bw.WriteUInt16(m_particlesA_14);
            bw.WriteUInt16(m_particlesA_15);
            bw.WriteUInt16(m_particlesB_0);
            bw.WriteUInt16(m_particlesB_1);
            bw.WriteUInt16(m_particlesB_2);
            bw.WriteUInt16(m_particlesB_3);
            bw.WriteUInt16(m_particlesB_4);
            bw.WriteUInt16(m_particlesB_5);
            bw.WriteUInt16(m_particlesB_6);
            bw.WriteUInt16(m_particlesB_7);
            bw.WriteUInt16(m_particlesB_8);
            bw.WriteUInt16(m_particlesB_9);
            bw.WriteUInt16(m_particlesB_10);
            bw.WriteUInt16(m_particlesB_11);
            bw.WriteUInt16(m_particlesB_12);
            bw.WriteUInt16(m_particlesB_13);
            bw.WriteUInt16(m_particlesB_14);
            bw.WriteUInt16(m_particlesB_15);
        }
    }
}