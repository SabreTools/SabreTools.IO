using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SabreTools.IO.Compression.zlib
{
    #pragma warning disable CS8981 // The type name only contains lower-cased ascii characters.
    public unsafe partial class ZLib
    {
        public enum block_state
        {
            need_more = 0,      /* block not completed, need more input or more output */
            block_done = 1,     /* block flush performed */
            finish_started = 2, /* finish started, need only more output at next deflate */
            finish_done = 3     /* finish done, accept no more input or output */
        }

        public enum codetype
        {
            CODES = 0,
            LENS = 1,
            DISTS = 2
        }

        public enum inflate_mode
        {
            HEAD = 16180,
            FLAGS = 16181,
            TIME = 16182,
            OS = 16183,
            EXLEN = 16184,
            EXTRA = 16185,
            NAME = 16186,
            COMMENT = 16187,
            HCRC = 16188,
            DICTID = 16189,
            DICT = 16190,
            TYPE = 16191,
            TYPEDO = 16192,
            STORED = 16193,
            COPY_ = 16194,
            COPY = 16195,
            TABLE = 16196,
            LENLENS = 16197,
            CODELENS = 16198,
            LEN_ = 16199,
            LEN = 16200,
            LENEXT = 16201,
            DIST = 16202,
            DISTEXT = 16203,
            MATCH = 16204,
            LIT = 16205,
            CHECK = 16206,
            LENGTH = 16207,
            DONE = 16208,
            BAD = 16209,
            MEM = 16210,
            SYNC = 16211,
        }

        public delegate void* delegate0(void* arg0, uint arg1, uint arg2);
        public delegate void delegate1(void* arg0, void* arg1);
        public delegate block_state delegate2(internal_state arg0, int arg1);
        public unsafe class internal_state
        {
            public z_stream_s? strm;
            public int status;
            public UnsafeArray1D<byte>? pending_bufArray;
            public byte* pending_buf;
            public uint pending_buf_size;
            public byte* pending_out;
            public uint pending;
            public int wrap;
            public gz_header_s* gzhead;
            public uint gzindex;
            public byte method;
            public int last_flush;
            public uint w_size;
            public uint w_bits;
            public uint w_mask;
            public UnsafeArray1D<byte>? windowArray;
            public byte* window;
            public uint window_size;
            public UnsafeArray1D<ushort>? prevArray;
            public ushort* prev;
            public UnsafeArray1D<ushort>? headArray;
            public ushort* head;
            public uint ins_h;
            public uint hash_size;
            public uint hash_bits;
            public uint hash_mask;
            public uint hash_shift;
            public int block_start;
            public uint match_length;
            public uint prev_match;
            public int match_available;
            public uint strstart;
            public uint match_start;
            public uint lookahead;
            public uint prev_length;
            public uint max_chain_length;
            public uint max_lazy_match;
            public int level;
            public int strategy;
            public uint good_match;
            public int nice_match;
            public UnsafeArray1D<ct_data_s> dyn_ltreeArray = new UnsafeArray1D<ct_data_s>(573);
            public ct_data_s* dyn_ltree;
            public UnsafeArray1D<ct_data_s> dyn_dtreeArray = new UnsafeArray1D<ct_data_s>(61);
            public ct_data_s* dyn_dtree;
            public UnsafeArray1D<ct_data_s> bl_treeArray = new UnsafeArray1D<ct_data_s>(39);
            public ct_data_s* bl_tree;
            public tree_desc_s l_desc;
            public tree_desc_s d_desc;
            public tree_desc_s bl_desc;
            public UnsafeArray1D<ushort> bl_countArray = new UnsafeArray1D<ushort>(16);
            public ushort* bl_count;
            public UnsafeArray1D<int> heapArray = new UnsafeArray1D<int>(573);
            public int* heap;
            public int heap_len;
            public int heap_max;
            public UnsafeArray1D<byte> depthArray = new UnsafeArray1D<byte>(573);
            public byte* depth;
            public byte* sym_buf;
            public uint lit_bufsize;
            public uint sym_next;
            public uint sym_end;
            public uint opt_len;
            public uint static_len;
            public uint matches;
            public uint insert;
            public ushort bi_buf;
            public int bi_valid;
            public uint high_water;
            public internal_state()
            {
                dyn_ltree = (ct_data_s*)dyn_ltreeArray;
                dyn_dtree = (ct_data_s*)dyn_dtreeArray;
                bl_tree = (ct_data_s*)bl_treeArray;
                bl_count = (ushort*)bl_countArray;
                heap = (int*)heapArray;
                depth = (byte*)depthArray;
                l_desc = new tree_desc_s();
                d_desc = new tree_desc_s();
                bl_desc = new tree_desc_s();
            }
        }

        public unsafe class z_stream_s
        {
            public byte* next_in;
            public uint avail_in;
            public uint total_in;
            public byte* next_out;
            public uint avail_out;
            public uint total_out;
            public string? msg; //Nanook was sbyte*
            public internal_state? state;
            public inflate_state? i_state;
            //public delegate0 zalloc; //Nanook all done through UnsafeArray
            //public delegate1 zfree; //Nanook all done through UnsafeArray
            //public void* opaque;
            public int data_type;
            public uint adler;
            public uint reserved;
        }

        public unsafe struct gz_header_s
        {
            public int text;
            public uint time;
            public int xflags;
            public int os;
            public byte* extra;
            public uint extra_len;
            public uint extra_max;
            public byte* name;
            public uint name_max;
            public byte* comment;
            public uint comm_max;
            public int hcrc;
            public int done;
        }

        public unsafe struct gzFile_s
        {
            public uint have;
            public byte* next;
            public long pos;
        }

        public unsafe class gz_state
        {
            public gzFile_s x;
            public int mode;
            public int fd;
            public sbyte* path;
            public uint size;
            public uint want;
            public byte* _in_;
            public byte* _out_;
            public int direct;
            public int how;
            public long start;
            public int eof;
            public int past;
            public int level;
            public int strategy;
            public int reset;
            public long skip;
            public int seek;
            public int err;
            public sbyte* msg;
            public z_stream_s? strm;
        }

        public unsafe struct ct_data_s
        {
            [StructLayout(LayoutKind.Explicit)]
            public unsafe struct unnamed1
            {
                [FieldOffset(0)]
                public ushort freq;
                [FieldOffset(0)]
                public ushort code;
            }

            public unnamed1 fc;
            [StructLayout(LayoutKind.Explicit)]
            public unsafe struct unnamed2
            {
                [FieldOffset(0)]
                public ushort dad;
                [FieldOffset(0)]
                public ushort len;
            }

            public unnamed2 dl;

            public override string ToString()
            {
                return $"freq:{fc.freq}, len:{dl.len}";
            }
        }

        public unsafe struct static_tree_desc_s
        {
            public ct_data_s[] static_tree; //Nanook was ct_data_s*
            public int[] extra_bits; //Nanook was int*
            public int extra_base;
            public int elems;
            public int max_length;
        }

        public unsafe class tree_desc_s
        {
            public ct_data_s[]? dyn_tree;  //Nanook was ct_data_s*
            public int max_code;
            public static_tree_desc_s stat_desc; //Nanook was static_tree_desc_s*
        }

        public unsafe class config_s
        {
            public config_s(ushort good_length, ushort max_lazy, ushort nice_length, ushort max_chain, delegate2 func)
            {
                this.good_length = good_length;
                this.max_lazy = max_lazy;
                this.nice_length = nice_length;
                this.max_chain = max_chain;
                this.func = func;
            }

            public ushort good_length;
            public ushort max_lazy;
            public ushort nice_length;
            public ushort max_chain;
            public delegate2 func;
        }

        public unsafe struct code
        {
            public byte op;
            public byte bits;
            public ushort val;
        }

        public unsafe class inflate_state
        {
            public z_stream_s? strm;
            //public unsafe struct inflate_mode
            //{
            //}

            public inflate_mode mode;
            public int last;
            public int wrap;
            public int havedict;
            public int flags;
            public uint dmax;
            public uint check;
            public uint total;
            public UnsafeArray1D<ushort>? headArray;
            public gz_header_s* head;
            public uint wbits;
            public uint wsize;
            public uint whave;
            public uint wnext;
            public UnsafeArray1D<byte>? windowArray;
            public byte* window;
            public uint hold;
            public uint bits;
            public uint length;
            public uint offset;
            public uint extra;
            public int lencode; //Nanook was code*
            public code[]? lencodes;
            public int distcode; //Nanook was code*
            public uint lenbits;
            public code[]? distcodes;
            public uint distbits;
            public uint ncode;
            public uint nlen;
            public uint ndist;
            public uint have;
            public int next;
            public UnsafeArray1D<ushort> lensArray = new UnsafeArray1D<ushort>(320);
            public ushort* lens;
            public UnsafeArray1D<ushort> workArray = new UnsafeArray1D<ushort>(288);
            public ushort* work;
            public UnsafeArray1D<code> codesArray = new UnsafeArray1D<code>(1444);
            public int codes;
            public int sane;
            public int back;
            public uint was;
            public inflate_state()
            {
                lens = (ushort*)lensArray;
                work = (ushort*)workArray;
                codes = 0; //(code*)codesArray; //Nanook index not pointer
            }
        }

        public static string[] z_errmsg = new string[]{"need dictionary", "stream end", "", "file error", "stream error", "data error", "insufficient memory", "buffer error", "incompatible version", ""};
        public static uint[] crc_table = new uint[]{0x00000000, 0x77073096, 0xee0e612c, 0x990951ba, 0x076dc419, 0x706af48f, 0xe963a535, 0x9e6495a3, 0x0edb8832, 0x79dcb8a4, 0xe0d5e91e, 0x97d2d988, 0x09b64c2b, 0x7eb17cbd, 0xe7b82d07, 0x90bf1d91, 0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de, 0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 0x136c9856, 0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9, 0xfa0f3d63, 0x8d080df5, 0x3b6e20c8, 0x4c69105e, 0xd56041e4, 0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b, 0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3, 0x45df5c75, 0xdcd60dcf, 0xabd13d59, 0x26d930ac, 0x51de003a, 0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599, 0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924, 0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d, 0x76dc4190, 0x01db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x06b6b51f, 0x9fbfe4a5, 0xe8b8d433, 0x7807c9a2, 0x0f00f934, 0x9609a88e, 0xe10e9818, 0x7f6a0dbb, 0x086d3d2d, 0x91646c97, 0xe6635c01, 0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed, 0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950, 0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3, 0xfbd44c65, 0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2, 0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a, 0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5, 0xaa0a4c5f, 0xdd0d7cc9, 0x5005713c, 0x270241aa, 0xbe0b1010, 0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f, 0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17, 0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad, 0xedb88320, 0x9abfb3b6, 0x03b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x04db2615, 0x73dc1683, 0xe3630b12, 0x94643b84, 0x0d6d6a3e, 0x7a6a5aa8, 0xe40ecf0b, 0x9309ff9d, 0x0a00ae27, 0x7d079eb1, 0xf00f9344, 0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb, 0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a, 0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5, 0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1, 0xa6bc5767, 0x3fb506dd, 0x48b2364b, 0xd80d2bda, 0xaf0a1b4c, 0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef, 0x4669be79, 0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236, 0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe, 0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31, 0x2cd99e8b, 0x5bdeae1d, 0x9b64c2b0, 0xec63f226, 0x756aa39c, 0x026d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x05005713, 0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0x0cb61b38, 0x92d28e9b, 0xe5d5be0d, 0x7cdcefb7, 0x0bdbdf21, 0x86d3d2d4, 0xf1d4e242, 0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1, 0x18b74777, 0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c, 0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45, 0xa00ae278, 0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7, 0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc, 0x40df0b66, 0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9, 0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605, 0xcdd70693, 0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8, 0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b, 0x2d02ef8d};
        public static ulong[] crc_big_table = new ulong[]{0x0000000000000000, 0x9630077700000000, 0x2c610eee00000000, 0xba51099900000000, 0x19c46d0700000000, 0x8ff46a7000000000, 0x35a563e900000000, 0xa395649e00000000, 0x3288db0e00000000, 0xa4b8dc7900000000, 0x1ee9d5e000000000, 0x88d9d29700000000, 0x2b4cb60900000000, 0xbd7cb17e00000000, 0x072db8e700000000, 0x911dbf9000000000, 0x6410b71d00000000, 0xf220b06a00000000, 0x4871b9f300000000, 0xde41be8400000000, 0x7dd4da1a00000000, 0xebe4dd6d00000000, 0x51b5d4f400000000, 0xc785d38300000000, 0x56986c1300000000, 0xc0a86b6400000000, 0x7af962fd00000000, 0xecc9658a00000000, 0x4f5c011400000000, 0xd96c066300000000, 0x633d0ffa00000000, 0xf50d088d00000000, 0xc8206e3b00000000, 0x5e10694c00000000, 0xe44160d500000000, 0x727167a200000000, 0xd1e4033c00000000, 0x47d4044b00000000, 0xfd850dd200000000, 0x6bb50aa500000000, 0xfaa8b53500000000, 0x6c98b24200000000, 0xd6c9bbdb00000000, 0x40f9bcac00000000, 0xe36cd83200000000, 0x755cdf4500000000, 0xcf0dd6dc00000000, 0x593dd1ab00000000, 0xac30d92600000000, 0x3a00de5100000000, 0x8051d7c800000000, 0x1661d0bf00000000, 0xb5f4b42100000000, 0x23c4b35600000000, 0x9995bacf00000000, 0x0fa5bdb800000000, 0x9eb8022800000000, 0x0888055f00000000, 0xb2d90cc600000000, 0x24e90bb100000000, 0x877c6f2f00000000, 0x114c685800000000, 0xab1d61c100000000, 0x3d2d66b600000000, 0x9041dc7600000000, 0x0671db0100000000, 0xbc20d29800000000, 0x2a10d5ef00000000, 0x8985b17100000000, 0x1fb5b60600000000, 0xa5e4bf9f00000000, 0x33d4b8e800000000, 0xa2c9077800000000, 0x34f9000f00000000, 0x8ea8099600000000, 0x18980ee100000000, 0xbb0d6a7f00000000, 0x2d3d6d0800000000, 0x976c649100000000, 0x015c63e600000000, 0xf4516b6b00000000, 0x62616c1c00000000, 0xd830658500000000, 0x4e0062f200000000, 0xed95066c00000000, 0x7ba5011b00000000, 0xc1f4088200000000, 0x57c40ff500000000, 0xc6d9b06500000000, 0x50e9b71200000000, 0xeab8be8b00000000, 0x7c88b9fc00000000, 0xdf1ddd6200000000, 0x492dda1500000000, 0xf37cd38c00000000, 0x654cd4fb00000000, 0x5861b24d00000000, 0xce51b53a00000000, 0x7400bca300000000, 0xe230bbd400000000, 0x41a5df4a00000000, 0xd795d83d00000000, 0x6dc4d1a400000000, 0xfbf4d6d300000000, 0x6ae9694300000000, 0xfcd96e3400000000, 0x468867ad00000000, 0xd0b860da00000000, 0x732d044400000000, 0xe51d033300000000, 0x5f4c0aaa00000000, 0xc97c0ddd00000000, 0x3c71055000000000, 0xaa41022700000000, 0x10100bbe00000000, 0x86200cc900000000, 0x25b5685700000000, 0xb3856f2000000000, 0x09d466b900000000, 0x9fe461ce00000000, 0x0ef9de5e00000000, 0x98c9d92900000000, 0x2298d0b000000000, 0xb4a8d7c700000000, 0x173db35900000000, 0x810db42e00000000, 0x3b5cbdb700000000, 0xad6cbac000000000, 0x2083b8ed00000000, 0xb6b3bf9a00000000, 0x0ce2b60300000000, 0x9ad2b17400000000, 0x3947d5ea00000000, 0xaf77d29d00000000, 0x1526db0400000000, 0x8316dc7300000000, 0x120b63e300000000, 0x843b649400000000, 0x3e6a6d0d00000000, 0xa85a6a7a00000000, 0x0bcf0ee400000000, 0x9dff099300000000, 0x27ae000a00000000, 0xb19e077d00000000, 0x44930ff000000000, 0xd2a3088700000000, 0x68f2011e00000000, 0xfec2066900000000, 0x5d5762f700000000, 0xcb67658000000000, 0x71366c1900000000, 0xe7066b6e00000000, 0x761bd4fe00000000, 0xe02bd38900000000, 0x5a7ada1000000000, 0xcc4add6700000000, 0x6fdfb9f900000000, 0xf9efbe8e00000000, 0x43beb71700000000, 0xd58eb06000000000, 0xe8a3d6d600000000, 0x7e93d1a100000000, 0xc4c2d83800000000, 0x52f2df4f00000000, 0xf167bbd100000000, 0x6757bca600000000, 0xdd06b53f00000000, 0x4b36b24800000000, 0xda2b0dd800000000, 0x4c1b0aaf00000000, 0xf64a033600000000, 0x607a044100000000, 0xc3ef60df00000000, 0x55df67a800000000, 0xef8e6e3100000000, 0x79be694600000000, 0x8cb361cb00000000, 0x1a8366bc00000000, 0xa0d26f2500000000, 0x36e2685200000000, 0x95770ccc00000000, 0x03470bbb00000000, 0xb916022200000000, 0x2f26055500000000, 0xbe3bbac500000000, 0x280bbdb200000000, 0x925ab42b00000000, 0x046ab35c00000000, 0xa7ffd7c200000000, 0x31cfd0b500000000, 0x8b9ed92c00000000, 0x1daede5b00000000, 0xb0c2649b00000000, 0x26f263ec00000000, 0x9ca36a7500000000, 0x0a936d0200000000, 0xa906099c00000000, 0x3f360eeb00000000, 0x8567077200000000, 0x1357000500000000, 0x824abf9500000000, 0x147ab8e200000000, 0xae2bb17b00000000, 0x381bb60c00000000, 0x9b8ed29200000000, 0x0dbed5e500000000, 0xb7efdc7c00000000, 0x21dfdb0b00000000, 0xd4d2d38600000000, 0x42e2d4f100000000, 0xf8b3dd6800000000, 0x6e83da1f00000000, 0xcd16be8100000000, 0x5b26b9f600000000, 0xe177b06f00000000, 0x7747b71800000000, 0xe65a088800000000, 0x706a0fff00000000, 0xca3b066600000000, 0x5c0b011100000000, 0xff9e658f00000000, 0x69ae62f800000000, 0xd3ff6b6100000000, 0x45cf6c1600000000, 0x78e20aa000000000, 0xeed20dd700000000, 0x5483044e00000000, 0xc2b3033900000000, 0x612667a700000000, 0xf71660d000000000, 0x4d47694900000000, 0xdb776e3e00000000, 0x4a6ad1ae00000000, 0xdc5ad6d900000000, 0x660bdf4000000000, 0xf03bd83700000000, 0x53aebca900000000, 0xc59ebbde00000000, 0x7fcfb24700000000, 0xe9ffb53000000000, 0x1cf2bdbd00000000, 0x8ac2baca00000000, 0x3093b35300000000, 0xa6a3b42400000000, 0x0536d0ba00000000, 0x9306d7cd00000000, 0x2957de5400000000, 0xbf67d92300000000, 0x2e7a66b300000000, 0xb84a61c400000000, 0x021b685d00000000, 0x942b6f2a00000000, 0x37be0bb400000000, 0xa18e0cc300000000, 0x1bdf055a00000000, 0x8def022d00000000};
        public static uint[][] crc_braid_table = new uint[][]
        {
             new uint[]{0x00000000, 0xaf449247, 0x85f822cf, 0x2abcb088, 0xd08143df, 0x7fc5d198, 0x55796110, 0xfa3df357, 0x7a7381ff, 0xd53713b8, 0xff8ba330, 0x50cf3177, 0xaaf2c220, 0x05b65067, 0x2f0ae0ef, 0x804e72a8, 0xf4e703fe, 0x5ba391b9, 0x711f2131, 0xde5bb376, 0x24664021, 0x8b22d266, 0xa19e62ee, 0x0edaf0a9, 0x8e948201, 0x21d01046, 0x0b6ca0ce, 0xa4283289, 0x5e15c1de, 0xf1515399, 0xdbede311, 0x74a97156, 0x32bf01bd, 0x9dfb93fa, 0xb7472372, 0x1803b135, 0xe23e4262, 0x4d7ad025, 0x67c660ad, 0xc882f2ea, 0x48cc8042, 0xe7881205, 0xcd34a28d, 0x627030ca, 0x984dc39d, 0x370951da, 0x1db5e152, 0xb2f17315, 0xc6580243, 0x691c9004, 0x43a0208c, 0xece4b2cb, 0x16d9419c, 0xb99dd3db, 0x93216353, 0x3c65f114, 0xbc2b83bc, 0x136f11fb, 0x39d3a173, 0x96973334, 0x6caac063, 0xc3ee5224, 0xe952e2ac, 0x461670eb, 0x657e037a, 0xca3a913d, 0xe08621b5, 0x4fc2b3f2, 0xb5ff40a5, 0x1abbd2e2, 0x3007626a, 0x9f43f02d, 0x1f0d8285, 0xb04910c2, 0x9af5a04a, 0x35b1320d, 0xcf8cc15a, 0x60c8531d, 0x4a74e395, 0xe53071d2, 0x91990084, 0x3edd92c3, 0x1461224b, 0xbb25b00c, 0x4118435b, 0xee5cd11c, 0xc4e06194, 0x6ba4f3d3, 0xebea817b, 0x44ae133c, 0x6e12a3b4, 0xc15631f3, 0x3b6bc2a4, 0x942f50e3, 0xbe93e06b, 0x11d7722c, 0x57c102c7, 0xf8859080, 0xd2392008, 0x7d7db24f, 0x87404118, 0x2804d35f, 0x02b863d7, 0xadfcf190, 0x2db28338, 0x82f6117f, 0xa84aa1f7, 0x070e33b0, 0xfd33c0e7, 0x527752a0, 0x78cbe228, 0xd78f706f, 0xa3260139, 0x0c62937e, 0x26de23f6, 0x899ab1b1, 0x73a742e6, 0xdce3d0a1, 0xf65f6029, 0x591bf26e, 0xd95580c6, 0x76111281, 0x5cada209, 0xf3e9304e, 0x09d4c319, 0xa690515e, 0x8c2ce1d6, 0x23687391, 0xcafc06f4, 0x65b894b3, 0x4f04243b, 0xe040b67c, 0x1a7d452b, 0xb539d76c, 0x9f8567e4, 0x30c1f5a3, 0xb08f870b, 0x1fcb154c, 0x3577a5c4, 0x9a333783, 0x600ec4d4, 0xcf4a5693, 0xe5f6e61b, 0x4ab2745c, 0x3e1b050a, 0x915f974d, 0xbbe327c5, 0x14a7b582, 0xee9a46d5, 0x41ded492, 0x6b62641a, 0xc426f65d, 0x446884f5, 0xeb2c16b2, 0xc190a63a, 0x6ed4347d, 0x94e9c72a, 0x3bad556d, 0x1111e5e5, 0xbe5577a2, 0xf8430749, 0x5707950e, 0x7dbb2586, 0xd2ffb7c1, 0x28c24496, 0x8786d6d1, 0xad3a6659, 0x027ef41e, 0x823086b6, 0x2d7414f1, 0x07c8a479, 0xa88c363e, 0x52b1c569, 0xfdf5572e, 0xd749e7a6, 0x780d75e1, 0x0ca404b7, 0xa3e096f0, 0x895c2678, 0x2618b43f, 0xdc254768, 0x7361d52f, 0x59dd65a7, 0xf699f7e0, 0x76d78548, 0xd993170f, 0xf32fa787, 0x5c6b35c0, 0xa656c697, 0x091254d0, 0x23aee458, 0x8cea761f, 0xaf82058e, 0x00c697c9, 0x2a7a2741, 0x853eb506, 0x7f034651, 0xd047d416, 0xfafb649e, 0x55bff6d9, 0xd5f18471, 0x7ab51636, 0x5009a6be, 0xff4d34f9, 0x0570c7ae, 0xaa3455e9, 0x8088e561, 0x2fcc7726, 0x5b650670, 0xf4219437, 0xde9d24bf, 0x71d9b6f8, 0x8be445af, 0x24a0d7e8, 0x0e1c6760, 0xa158f527, 0x2116878f, 0x8e5215c8, 0xa4eea540, 0x0baa3707, 0xf197c450, 0x5ed35617, 0x746fe69f, 0xdb2b74d8, 0x9d3d0433, 0x32799674, 0x18c526fc, 0xb781b4bb, 0x4dbc47ec, 0xe2f8d5ab, 0xc8446523, 0x6700f764, 0xe74e85cc, 0x480a178b, 0x62b6a703, 0xcdf23544, 0x37cfc613, 0x988b5454, 0xb237e4dc, 0x1d73769b, 0x69da07cd, 0xc69e958a, 0xec222502, 0x4366b745, 0xb95b4412, 0x161fd655, 0x3ca366dd, 0x93e7f49a, 0x13a98632, 0xbced1475, 0x9651a4fd, 0x391536ba, 0xc328c5ed, 0x6c6c57aa, 0x46d0e722, 0xe9947565},
             new uint[]{0x00000000, 0x4e890ba9, 0x9d121752, 0xd39b1cfb, 0xe15528e5, 0xafdc234c, 0x7c473fb7, 0x32ce341e, 0x19db578b, 0x57525c22, 0x84c940d9, 0xca404b70, 0xf88e7f6e, 0xb60774c7, 0x659c683c, 0x2b156395, 0x33b6af16, 0x7d3fa4bf, 0xaea4b844, 0xe02db3ed, 0xd2e387f3, 0x9c6a8c5a, 0x4ff190a1, 0x01789b08, 0x2a6df89d, 0x64e4f334, 0xb77fefcf, 0xf9f6e466, 0xcb38d078, 0x85b1dbd1, 0x562ac72a, 0x18a3cc83, 0x676d5e2c, 0x29e45585, 0xfa7f497e, 0xb4f642d7, 0x863876c9, 0xc8b17d60, 0x1b2a619b, 0x55a36a32, 0x7eb609a7, 0x303f020e, 0xe3a41ef5, 0xad2d155c, 0x9fe32142, 0xd16a2aeb, 0x02f13610, 0x4c783db9, 0x54dbf13a, 0x1a52fa93, 0xc9c9e668, 0x8740edc1, 0xb58ed9df, 0xfb07d276, 0x289cce8d, 0x6615c524, 0x4d00a6b1, 0x0389ad18, 0xd012b1e3, 0x9e9bba4a, 0xac558e54, 0xe2dc85fd, 0x31479906, 0x7fce92af, 0xcedabc58, 0x8053b7f1, 0x53c8ab0a, 0x1d41a0a3, 0x2f8f94bd, 0x61069f14, 0xb29d83ef, 0xfc148846, 0xd701ebd3, 0x9988e07a, 0x4a13fc81, 0x049af728, 0x3654c336, 0x78ddc89f, 0xab46d464, 0xe5cfdfcd, 0xfd6c134e, 0xb3e518e7, 0x607e041c, 0x2ef70fb5, 0x1c393bab, 0x52b03002, 0x812b2cf9, 0xcfa22750, 0xe4b744c5, 0xaa3e4f6c, 0x79a55397, 0x372c583e, 0x05e26c20, 0x4b6b6789, 0x98f07b72, 0xd67970db, 0xa9b7e274, 0xe73ee9dd, 0x34a5f526, 0x7a2cfe8f, 0x48e2ca91, 0x066bc138, 0xd5f0ddc3, 0x9b79d66a, 0xb06cb5ff, 0xfee5be56, 0x2d7ea2ad, 0x63f7a904, 0x51399d1a, 0x1fb096b3, 0xcc2b8a48, 0x82a281e1, 0x9a014d62, 0xd48846cb, 0x07135a30, 0x499a5199, 0x7b546587, 0x35dd6e2e, 0xe64672d5, 0xa8cf797c, 0x83da1ae9, 0xcd531140, 0x1ec80dbb, 0x50410612, 0x628f320c, 0x2c0639a5, 0xff9d255e, 0xb1142ef7, 0x46c47ef1, 0x084d7558, 0xdbd669a3, 0x955f620a, 0xa7915614, 0xe9185dbd, 0x3a834146, 0x740a4aef, 0x5f1f297a, 0x119622d3, 0xc20d3e28, 0x8c843581, 0xbe4a019f, 0xf0c30a36, 0x235816cd, 0x6dd11d64, 0x7572d1e7, 0x3bfbda4e, 0xe860c6b5, 0xa6e9cd1c, 0x9427f902, 0xdaaef2ab, 0x0935ee50, 0x47bce5f9, 0x6ca9866c, 0x22208dc5, 0xf1bb913e, 0xbf329a97, 0x8dfcae89, 0xc375a520, 0x10eeb9db, 0x5e67b272, 0x21a920dd, 0x6f202b74, 0xbcbb378f, 0xf2323c26, 0xc0fc0838, 0x8e750391, 0x5dee1f6a, 0x136714c3, 0x38727756, 0x76fb7cff, 0xa5606004, 0xebe96bad, 0xd9275fb3, 0x97ae541a, 0x443548e1, 0x0abc4348, 0x121f8fcb, 0x5c968462, 0x8f0d9899, 0xc1849330, 0xf34aa72e, 0xbdc3ac87, 0x6e58b07c, 0x20d1bbd5, 0x0bc4d840, 0x454dd3e9, 0x96d6cf12, 0xd85fc4bb, 0xea91f0a5, 0xa418fb0c, 0x7783e7f7, 0x390aec5e, 0x881ec2a9, 0xc697c900, 0x150cd5fb, 0x5b85de52, 0x694bea4c, 0x27c2e1e5, 0xf459fd1e, 0xbad0f6b7, 0x91c59522, 0xdf4c9e8b, 0x0cd78270, 0x425e89d9, 0x7090bdc7, 0x3e19b66e, 0xed82aa95, 0xa30ba13c, 0xbba86dbf, 0xf5216616, 0x26ba7aed, 0x68337144, 0x5afd455a, 0x14744ef3, 0xc7ef5208, 0x896659a1, 0xa2733a34, 0xecfa319d, 0x3f612d66, 0x71e826cf, 0x432612d1, 0x0daf1978, 0xde340583, 0x90bd0e2a, 0xef739c85, 0xa1fa972c, 0x72618bd7, 0x3ce8807e, 0x0e26b460, 0x40afbfc9, 0x9334a332, 0xddbda89b, 0xf6a8cb0e, 0xb821c0a7, 0x6bbadc5c, 0x2533d7f5, 0x17fde3eb, 0x5974e842, 0x8aeff4b9, 0xc466ff10, 0xdcc53393, 0x924c383a, 0x41d724c1, 0x0f5e2f68, 0x3d901b76, 0x731910df, 0xa0820c24, 0xee0b078d, 0xc51e6418, 0x8b976fb1, 0x580c734a, 0x168578e3, 0x244b4cfd, 0x6ac24754, 0xb9595baf, 0xf7d05006},
             new uint[]{0x00000000, 0x8d88fde2, 0xc060fd85, 0x4de80067, 0x5bb0fd4b, 0xd63800a9, 0x9bd000ce, 0x1658fd2c, 0xb761fa96, 0x3ae90774, 0x77010713, 0xfa89faf1, 0xecd107dd, 0x6159fa3f, 0x2cb1fa58, 0xa13907ba, 0xb5b2f36d, 0x383a0e8f, 0x75d20ee8, 0xf85af30a, 0xee020e26, 0x638af3c4, 0x2e62f3a3, 0xa3ea0e41, 0x02d309fb, 0x8f5bf419, 0xc2b3f47e, 0x4f3b099c, 0x5963f4b0, 0xd4eb0952, 0x99030935, 0x148bf4d7, 0xb014e09b, 0x3d9c1d79, 0x70741d1e, 0xfdfce0fc, 0xeba41dd0, 0x662ce032, 0x2bc4e055, 0xa64c1db7, 0x07751a0d, 0x8afde7ef, 0xc715e788, 0x4a9d1a6a, 0x5cc5e746, 0xd14d1aa4, 0x9ca51ac3, 0x112de721, 0x05a613f6, 0x882eee14, 0xc5c6ee73, 0x484e1391, 0x5e16eebd, 0xd39e135f, 0x9e761338, 0x13feeeda, 0xb2c7e960, 0x3f4f1482, 0x72a714e5, 0xff2fe907, 0xe977142b, 0x64ffe9c9, 0x2917e9ae, 0xa49f144c, 0xbb58c777, 0x36d03a95, 0x7b383af2, 0xf6b0c710, 0xe0e83a3c, 0x6d60c7de, 0x2088c7b9, 0xad003a5b, 0x0c393de1, 0x81b1c003, 0xcc59c064, 0x41d13d86, 0x5789c0aa, 0xda013d48, 0x97e93d2f, 0x1a61c0cd, 0x0eea341a, 0x8362c9f8, 0xce8ac99f, 0x4302347d, 0x555ac951, 0xd8d234b3, 0x953a34d4, 0x18b2c936, 0xb98bce8c, 0x3403336e, 0x79eb3309, 0xf463ceeb, 0xe23b33c7, 0x6fb3ce25, 0x225bce42, 0xafd333a0, 0x0b4c27ec, 0x86c4da0e, 0xcb2cda69, 0x46a4278b, 0x50fcdaa7, 0xdd742745, 0x909c2722, 0x1d14dac0, 0xbc2ddd7a, 0x31a52098, 0x7c4d20ff, 0xf1c5dd1d, 0xe79d2031, 0x6a15ddd3, 0x27fdddb4, 0xaa752056, 0xbefed481, 0x33762963, 0x7e9e2904, 0xf316d4e6, 0xe54e29ca, 0x68c6d428, 0x252ed44f, 0xa8a629ad, 0x099f2e17, 0x8417d3f5, 0xc9ffd392, 0x44772e70, 0x522fd35c, 0xdfa72ebe, 0x924f2ed9, 0x1fc7d33b, 0xadc088af, 0x2048754d, 0x6da0752a, 0xe02888c8, 0xf67075e4, 0x7bf88806, 0x36108861, 0xbb987583, 0x1aa17239, 0x97298fdb, 0xdac18fbc, 0x5749725e, 0x41118f72, 0xcc997290, 0x817172f7, 0x0cf98f15, 0x18727bc2, 0x95fa8620, 0xd8128647, 0x559a7ba5, 0x43c28689, 0xce4a7b6b, 0x83a27b0c, 0x0e2a86ee, 0xaf138154, 0x229b7cb6, 0x6f737cd1, 0xe2fb8133, 0xf4a37c1f, 0x792b81fd, 0x34c3819a, 0xb94b7c78, 0x1dd46834, 0x905c95d6, 0xddb495b1, 0x503c6853, 0x4664957f, 0xcbec689d, 0x860468fa, 0x0b8c9518, 0xaab592a2, 0x273d6f40, 0x6ad56f27, 0xe75d92c5, 0xf1056fe9, 0x7c8d920b, 0x3165926c, 0xbced6f8e, 0xa8669b59, 0x25ee66bb, 0x680666dc, 0xe58e9b3e, 0xf3d66612, 0x7e5e9bf0, 0x33b69b97, 0xbe3e6675, 0x1f0761cf, 0x928f9c2d, 0xdf679c4a, 0x52ef61a8, 0x44b79c84, 0xc93f6166, 0x84d76101, 0x095f9ce3, 0x16984fd8, 0x9b10b23a, 0xd6f8b25d, 0x5b704fbf, 0x4d28b293, 0xc0a04f71, 0x8d484f16, 0x00c0b2f4, 0xa1f9b54e, 0x2c7148ac, 0x619948cb, 0xec11b529, 0xfa494805, 0x77c1b5e7, 0x3a29b580, 0xb7a14862, 0xa32abcb5, 0x2ea24157, 0x634a4130, 0xeec2bcd2, 0xf89a41fe, 0x7512bc1c, 0x38fabc7b, 0xb5724199, 0x144b4623, 0x99c3bbc1, 0xd42bbba6, 0x59a34644, 0x4ffbbb68, 0xc273468a, 0x8f9b46ed, 0x0213bb0f, 0xa68caf43, 0x2b0452a1, 0x66ec52c6, 0xeb64af24, 0xfd3c5208, 0x70b4afea, 0x3d5caf8d, 0xb0d4526f, 0x11ed55d5, 0x9c65a837, 0xd18da850, 0x5c0555b2, 0x4a5da89e, 0xc7d5557c, 0x8a3d551b, 0x07b5a8f9, 0x133e5c2e, 0x9eb6a1cc, 0xd35ea1ab, 0x5ed65c49, 0x488ea165, 0xc5065c87, 0x88ee5ce0, 0x0566a102, 0xa45fa6b8, 0x29d75b5a, 0x643f5b3d, 0xe9b7a6df, 0xffef5bf3, 0x7267a611, 0x3f8fa676, 0xb2075b94},
             new uint[]{0x00000000, 0x80f0171f, 0xda91287f, 0x5a613f60, 0x6e5356bf, 0xeea341a0, 0xb4c27ec0, 0x343269df, 0xdca6ad7e, 0x5c56ba61, 0x06378501, 0x86c7921e, 0xb2f5fbc1, 0x3205ecde, 0x6864d3be, 0xe894c4a1, 0x623c5cbd, 0xe2cc4ba2, 0xb8ad74c2, 0x385d63dd, 0x0c6f0a02, 0x8c9f1d1d, 0xd6fe227d, 0x560e3562, 0xbe9af1c3, 0x3e6ae6dc, 0x640bd9bc, 0xe4fbcea3, 0xd0c9a77c, 0x5039b063, 0x0a588f03, 0x8aa8981c, 0xc478b97a, 0x4488ae65, 0x1ee99105, 0x9e19861a, 0xaa2befc5, 0x2adbf8da, 0x70bac7ba, 0xf04ad0a5, 0x18de1404, 0x982e031b, 0xc24f3c7b, 0x42bf2b64, 0x768d42bb, 0xf67d55a4, 0xac1c6ac4, 0x2cec7ddb, 0xa644e5c7, 0x26b4f2d8, 0x7cd5cdb8, 0xfc25daa7, 0xc817b378, 0x48e7a467, 0x12869b07, 0x92768c18, 0x7ae248b9, 0xfa125fa6, 0xa07360c6, 0x208377d9, 0x14b11e06, 0x94410919, 0xce203679, 0x4ed02166, 0x538074b5, 0xd37063aa, 0x89115cca, 0x09e14bd5, 0x3dd3220a, 0xbd233515, 0xe7420a75, 0x67b21d6a, 0x8f26d9cb, 0x0fd6ced4, 0x55b7f1b4, 0xd547e6ab, 0xe1758f74, 0x6185986b, 0x3be4a70b, 0xbb14b014, 0x31bc2808, 0xb14c3f17, 0xeb2d0077, 0x6bdd1768, 0x5fef7eb7, 0xdf1f69a8, 0x857e56c8, 0x058e41d7, 0xed1a8576, 0x6dea9269, 0x378bad09, 0xb77bba16, 0x8349d3c9, 0x03b9c4d6, 0x59d8fbb6, 0xd928eca9, 0x97f8cdcf, 0x1708dad0, 0x4d69e5b0, 0xcd99f2af, 0xf9ab9b70, 0x795b8c6f, 0x233ab30f, 0xa3caa410, 0x4b5e60b1, 0xcbae77ae, 0x91cf48ce, 0x113f5fd1, 0x250d360e, 0xa5fd2111, 0xff9c1e71, 0x7f6c096e, 0xf5c49172, 0x7534866d, 0x2f55b90d, 0xafa5ae12, 0x9b97c7cd, 0x1b67d0d2, 0x4106efb2, 0xc1f6f8ad, 0x29623c0c, 0xa9922b13, 0xf3f31473, 0x7303036c, 0x47316ab3, 0xc7c17dac, 0x9da042cc, 0x1d5055d3, 0xa700e96a, 0x27f0fe75, 0x7d91c115, 0xfd61d60a, 0xc953bfd5, 0x49a3a8ca, 0x13c297aa, 0x933280b5, 0x7ba64414, 0xfb56530b, 0xa1376c6b, 0x21c77b74, 0x15f512ab, 0x950505b4, 0xcf643ad4, 0x4f942dcb, 0xc53cb5d7, 0x45cca2c8, 0x1fad9da8, 0x9f5d8ab7, 0xab6fe368, 0x2b9ff477, 0x71fecb17, 0xf10edc08, 0x199a18a9, 0x996a0fb6, 0xc30b30d6, 0x43fb27c9, 0x77c94e16, 0xf7395909, 0xad586669, 0x2da87176, 0x63785010, 0xe388470f, 0xb9e9786f, 0x39196f70, 0x0d2b06af, 0x8ddb11b0, 0xd7ba2ed0, 0x574a39cf, 0xbfdefd6e, 0x3f2eea71, 0x654fd511, 0xe5bfc20e, 0xd18dabd1, 0x517dbcce, 0x0b1c83ae, 0x8bec94b1, 0x01440cad, 0x81b41bb2, 0xdbd524d2, 0x5b2533cd, 0x6f175a12, 0xefe74d0d, 0xb586726d, 0x35766572, 0xdde2a1d3, 0x5d12b6cc, 0x077389ac, 0x87839eb3, 0xb3b1f76c, 0x3341e073, 0x6920df13, 0xe9d0c80c, 0xf4809ddf, 0x74708ac0, 0x2e11b5a0, 0xaee1a2bf, 0x9ad3cb60, 0x1a23dc7f, 0x4042e31f, 0xc0b2f400, 0x282630a1, 0xa8d627be, 0xf2b718de, 0x72470fc1, 0x4675661e, 0xc6857101, 0x9ce44e61, 0x1c14597e, 0x96bcc162, 0x164cd67d, 0x4c2de91d, 0xccddfe02, 0xf8ef97dd, 0x781f80c2, 0x227ebfa2, 0xa28ea8bd, 0x4a1a6c1c, 0xcaea7b03, 0x908b4463, 0x107b537c, 0x24493aa3, 0xa4b92dbc, 0xfed812dc, 0x7e2805c3, 0x30f824a5, 0xb00833ba, 0xea690cda, 0x6a991bc5, 0x5eab721a, 0xde5b6505, 0x843a5a65, 0x04ca4d7a, 0xec5e89db, 0x6cae9ec4, 0x36cfa1a4, 0xb63fb6bb, 0x820ddf64, 0x02fdc87b, 0x589cf71b, 0xd86ce004, 0x52c47818, 0xd2346f07, 0x88555067, 0x08a54778, 0x3c972ea7, 0xbc6739b8, 0xe60606d8, 0x66f611c7, 0x8e62d566, 0x0e92c279, 0x54f3fd19, 0xd403ea06, 0xe03183d9, 0x60c194c6, 0x3aa0aba6, 0xba50bcb9},
             new uint[]{0x00000000, 0x9570d495, 0xf190af6b, 0x64e07bfe, 0x38505897, 0xad208c02, 0xc9c0f7fc, 0x5cb02369, 0x70a0b12e, 0xe5d065bb, 0x81301e45, 0x1440cad0, 0x48f0e9b9, 0xdd803d2c, 0xb96046d2, 0x2c109247, 0xe141625c, 0x7431b6c9, 0x10d1cd37, 0x85a119a2, 0xd9113acb, 0x4c61ee5e, 0x288195a0, 0xbdf14135, 0x91e1d372, 0x049107e7, 0x60717c19, 0xf501a88c, 0xa9b18be5, 0x3cc15f70, 0x5821248e, 0xcd51f01b, 0x19f3c2f9, 0x8c83166c, 0xe8636d92, 0x7d13b907, 0x21a39a6e, 0xb4d34efb, 0xd0333505, 0x4543e190, 0x695373d7, 0xfc23a742, 0x98c3dcbc, 0x0db30829, 0x51032b40, 0xc473ffd5, 0xa093842b, 0x35e350be, 0xf8b2a0a5, 0x6dc27430, 0x09220fce, 0x9c52db5b, 0xc0e2f832, 0x55922ca7, 0x31725759, 0xa40283cc, 0x8812118b, 0x1d62c51e, 0x7982bee0, 0xecf26a75, 0xb042491c, 0x25329d89, 0x41d2e677, 0xd4a232e2, 0x33e785f2, 0xa6975167, 0xc2772a99, 0x5707fe0c, 0x0bb7dd65, 0x9ec709f0, 0xfa27720e, 0x6f57a69b, 0x434734dc, 0xd637e049, 0xb2d79bb7, 0x27a74f22, 0x7b176c4b, 0xee67b8de, 0x8a87c320, 0x1ff717b5, 0xd2a6e7ae, 0x47d6333b, 0x233648c5, 0xb6469c50, 0xeaf6bf39, 0x7f866bac, 0x1b661052, 0x8e16c4c7, 0xa2065680, 0x37768215, 0x5396f9eb, 0xc6e62d7e, 0x9a560e17, 0x0f26da82, 0x6bc6a17c, 0xfeb675e9, 0x2a14470b, 0xbf64939e, 0xdb84e860, 0x4ef43cf5, 0x12441f9c, 0x8734cb09, 0xe3d4b0f7, 0x76a46462, 0x5ab4f625, 0xcfc422b0, 0xab24594e, 0x3e548ddb, 0x62e4aeb2, 0xf7947a27, 0x937401d9, 0x0604d54c, 0xcb552557, 0x5e25f1c2, 0x3ac58a3c, 0xafb55ea9, 0xf3057dc0, 0x6675a955, 0x0295d2ab, 0x97e5063e, 0xbbf59479, 0x2e8540ec, 0x4a653b12, 0xdf15ef87, 0x83a5ccee, 0x16d5187b, 0x72356385, 0xe745b710, 0x67cf0be4, 0xf2bfdf71, 0x965fa48f, 0x032f701a, 0x5f9f5373, 0xcaef87e6, 0xae0ffc18, 0x3b7f288d, 0x176fbaca, 0x821f6e5f, 0xe6ff15a1, 0x738fc134, 0x2f3fe25d, 0xba4f36c8, 0xdeaf4d36, 0x4bdf99a3, 0x868e69b8, 0x13febd2d, 0x771ec6d3, 0xe26e1246, 0xbede312f, 0x2baee5ba, 0x4f4e9e44, 0xda3e4ad1, 0xf62ed896, 0x635e0c03, 0x07be77fd, 0x92cea368, 0xce7e8001, 0x5b0e5494, 0x3fee2f6a, 0xaa9efbff, 0x7e3cc91d, 0xeb4c1d88, 0x8fac6676, 0x1adcb2e3, 0x466c918a, 0xd31c451f, 0xb7fc3ee1, 0x228cea74, 0x0e9c7833, 0x9becaca6, 0xff0cd758, 0x6a7c03cd, 0x36cc20a4, 0xa3bcf431, 0xc75c8fcf, 0x522c5b5a, 0x9f7dab41, 0x0a0d7fd4, 0x6eed042a, 0xfb9dd0bf, 0xa72df3d6, 0x325d2743, 0x56bd5cbd, 0xc3cd8828, 0xefdd1a6f, 0x7aadcefa, 0x1e4db504, 0x8b3d6191, 0xd78d42f8, 0x42fd966d, 0x261ded93, 0xb36d3906, 0x54288e16, 0xc1585a83, 0xa5b8217d, 0x30c8f5e8, 0x6c78d681, 0xf9080214, 0x9de879ea, 0x0898ad7f, 0x24883f38, 0xb1f8ebad, 0xd5189053, 0x406844c6, 0x1cd867af, 0x89a8b33a, 0xed48c8c4, 0x78381c51, 0xb569ec4a, 0x201938df, 0x44f94321, 0xd18997b4, 0x8d39b4dd, 0x18496048, 0x7ca91bb6, 0xe9d9cf23, 0xc5c95d64, 0x50b989f1, 0x3459f20f, 0xa129269a, 0xfd9905f3, 0x68e9d166, 0x0c09aa98, 0x99797e0d, 0x4ddb4cef, 0xd8ab987a, 0xbc4be384, 0x293b3711, 0x758b1478, 0xe0fbc0ed, 0x841bbb13, 0x116b6f86, 0x3d7bfdc1, 0xa80b2954, 0xcceb52aa, 0x599b863f, 0x052ba556, 0x905b71c3, 0xf4bb0a3d, 0x61cbdea8, 0xac9a2eb3, 0x39eafa26, 0x5d0a81d8, 0xc87a554d, 0x94ca7624, 0x01baa2b1, 0x655ad94f, 0xf02a0dda, 0xdc3a9f9d, 0x494a4b08, 0x2daa30f6, 0xb8dae463, 0xe46ac70a, 0x711a139f, 0x15fa6861, 0x808abcf4},
             new uint[]{0x00000000, 0xcf9e17c8, 0x444d29d1, 0x8bd33e19, 0x889a53a2, 0x4704446a, 0xccd77a73, 0x03496dbb, 0xca45a105, 0x05dbb6cd, 0x8e0888d4, 0x41969f1c, 0x42dff2a7, 0x8d41e56f, 0x0692db76, 0xc90cccbe, 0x4ffa444b, 0x80645383, 0x0bb76d9a, 0xc4297a52, 0xc76017e9, 0x08fe0021, 0x832d3e38, 0x4cb329f0, 0x85bfe54e, 0x4a21f286, 0xc1f2cc9f, 0x0e6cdb57, 0x0d25b6ec, 0xc2bba124, 0x49689f3d, 0x86f688f5, 0x9ff48896, 0x506a9f5e, 0xdbb9a147, 0x1427b68f, 0x176edb34, 0xd8f0ccfc, 0x5323f2e5, 0x9cbde52d, 0x55b12993, 0x9a2f3e5b, 0x11fc0042, 0xde62178a, 0xdd2b7a31, 0x12b56df9, 0x996653e0, 0x56f84428, 0xd00eccdd, 0x1f90db15, 0x9443e50c, 0x5bddf2c4, 0x58949f7f, 0x970a88b7, 0x1cd9b6ae, 0xd347a166, 0x1a4b6dd8, 0xd5d57a10, 0x5e064409, 0x919853c1, 0x92d13e7a, 0x5d4f29b2, 0xd69c17ab, 0x19020063, 0xe498176d, 0x2b0600a5, 0xa0d53ebc, 0x6f4b2974, 0x6c0244cf, 0xa39c5307, 0x284f6d1e, 0xe7d17ad6, 0x2eddb668, 0xe143a1a0, 0x6a909fb9, 0xa50e8871, 0xa647e5ca, 0x69d9f202, 0xe20acc1b, 0x2d94dbd3, 0xab625326, 0x64fc44ee, 0xef2f7af7, 0x20b16d3f, 0x23f80084, 0xec66174c, 0x67b52955, 0xa82b3e9d, 0x6127f223, 0xaeb9e5eb, 0x256adbf2, 0xeaf4cc3a, 0xe9bda181, 0x2623b649, 0xadf08850, 0x626e9f98, 0x7b6c9ffb, 0xb4f28833, 0x3f21b62a, 0xf0bfa1e2, 0xf3f6cc59, 0x3c68db91, 0xb7bbe588, 0x7825f240, 0xb1293efe, 0x7eb72936, 0xf564172f, 0x3afa00e7, 0x39b36d5c, 0xf62d7a94, 0x7dfe448d, 0xb2605345, 0x3496dbb0, 0xfb08cc78, 0x70dbf261, 0xbf45e5a9, 0xbc0c8812, 0x73929fda, 0xf841a1c3, 0x37dfb60b, 0xfed37ab5, 0x314d6d7d, 0xba9e5364, 0x750044ac, 0x76492917, 0xb9d73edf, 0x320400c6, 0xfd9a170e, 0x1241289b, 0xdddf3f53, 0x560c014a, 0x99921682, 0x9adb7b39, 0x55456cf1, 0xde9652e8, 0x11084520, 0xd804899e, 0x179a9e56, 0x9c49a04f, 0x53d7b787, 0x509eda3c, 0x9f00cdf4, 0x14d3f3ed, 0xdb4de425, 0x5dbb6cd0, 0x92257b18, 0x19f64501, 0xd66852c9, 0xd5213f72, 0x1abf28ba, 0x916c16a3, 0x5ef2016b, 0x97fecdd5, 0x5860da1d, 0xd3b3e404, 0x1c2df3cc, 0x1f649e77, 0xd0fa89bf, 0x5b29b7a6, 0x94b7a06e, 0x8db5a00d, 0x422bb7c5, 0xc9f889dc, 0x06669e14, 0x052ff3af, 0xcab1e467, 0x4162da7e, 0x8efccdb6, 0x47f00108, 0x886e16c0, 0x03bd28d9, 0xcc233f11, 0xcf6a52aa, 0x00f44562, 0x8b277b7b, 0x44b96cb3, 0xc24fe446, 0x0dd1f38e, 0x8602cd97, 0x499cda5f, 0x4ad5b7e4, 0x854ba02c, 0x0e989e35, 0xc10689fd, 0x080a4543, 0xc794528b, 0x4c476c92, 0x83d97b5a, 0x809016e1, 0x4f0e0129, 0xc4dd3f30, 0x0b4328f8, 0xf6d93ff6, 0x3947283e, 0xb2941627, 0x7d0a01ef, 0x7e436c54, 0xb1dd7b9c, 0x3a0e4585, 0xf590524d, 0x3c9c9ef3, 0xf302893b, 0x78d1b722, 0xb74fa0ea, 0xb406cd51, 0x7b98da99, 0xf04be480, 0x3fd5f348, 0xb9237bbd, 0x76bd6c75, 0xfd6e526c, 0x32f045a4, 0x31b9281f, 0xfe273fd7, 0x75f401ce, 0xba6a1606, 0x7366dab8, 0xbcf8cd70, 0x372bf369, 0xf8b5e4a1, 0xfbfc891a, 0x34629ed2, 0xbfb1a0cb, 0x702fb703, 0x692db760, 0xa6b3a0a8, 0x2d609eb1, 0xe2fe8979, 0xe1b7e4c2, 0x2e29f30a, 0xa5facd13, 0x6a64dadb, 0xa3681665, 0x6cf601ad, 0xe7253fb4, 0x28bb287c, 0x2bf245c7, 0xe46c520f, 0x6fbf6c16, 0xa0217bde, 0x26d7f32b, 0xe949e4e3, 0x629adafa, 0xad04cd32, 0xae4da089, 0x61d3b741, 0xea008958, 0x259e9e90, 0xec92522e, 0x230c45e6, 0xa8df7bff, 0x67416c37, 0x6408018c, 0xab961644, 0x2045285d, 0xefdb3f95},
             new uint[]{0x00000000, 0x24825136, 0x4904a26c, 0x6d86f35a, 0x920944d8, 0xb68b15ee, 0xdb0de6b4, 0xff8fb782, 0xff638ff1, 0xdbe1dec7, 0xb6672d9d, 0x92e57cab, 0x6d6acb29, 0x49e89a1f, 0x246e6945, 0x00ec3873, 0x25b619a3, 0x01344895, 0x6cb2bbcf, 0x4830eaf9, 0xb7bf5d7b, 0x933d0c4d, 0xfebbff17, 0xda39ae21, 0xdad59652, 0xfe57c764, 0x93d1343e, 0xb7536508, 0x48dcd28a, 0x6c5e83bc, 0x01d870e6, 0x255a21d0, 0x4b6c3346, 0x6fee6270, 0x0268912a, 0x26eac01c, 0xd965779e, 0xfde726a8, 0x9061d5f2, 0xb4e384c4, 0xb40fbcb7, 0x908ded81, 0xfd0b1edb, 0xd9894fed, 0x2606f86f, 0x0284a959, 0x6f025a03, 0x4b800b35, 0x6eda2ae5, 0x4a587bd3, 0x27de8889, 0x035cd9bf, 0xfcd36e3d, 0xd8513f0b, 0xb5d7cc51, 0x91559d67, 0x91b9a514, 0xb53bf422, 0xd8bd0778, 0xfc3f564e, 0x03b0e1cc, 0x2732b0fa, 0x4ab443a0, 0x6e361296, 0x96d8668c, 0xb25a37ba, 0xdfdcc4e0, 0xfb5e95d6, 0x04d12254, 0x20537362, 0x4dd58038, 0x6957d10e, 0x69bbe97d, 0x4d39b84b, 0x20bf4b11, 0x043d1a27, 0xfbb2ada5, 0xdf30fc93, 0xb2b60fc9, 0x96345eff, 0xb36e7f2f, 0x97ec2e19, 0xfa6add43, 0xdee88c75, 0x21673bf7, 0x05e56ac1, 0x6863999b, 0x4ce1c8ad, 0x4c0df0de, 0x688fa1e8, 0x050952b2, 0x218b0384, 0xde04b406, 0xfa86e530, 0x9700166a, 0xb382475c, 0xddb455ca, 0xf93604fc, 0x94b0f7a6, 0xb032a690, 0x4fbd1112, 0x6b3f4024, 0x06b9b37e, 0x223be248, 0x22d7da3b, 0x06558b0d, 0x6bd37857, 0x4f512961, 0xb0de9ee3, 0x945ccfd5, 0xf9da3c8f, 0xdd586db9, 0xf8024c69, 0xdc801d5f, 0xb106ee05, 0x9584bf33, 0x6a0b08b1, 0x4e895987, 0x230faadd, 0x078dfbeb, 0x0761c398, 0x23e392ae, 0x4e6561f4, 0x6ae730c2, 0x95688740, 0xb1ead676, 0xdc6c252c, 0xf8ee741a, 0xf6c1cb59, 0xd2439a6f, 0xbfc56935, 0x9b473803, 0x64c88f81, 0x404adeb7, 0x2dcc2ded, 0x094e7cdb, 0x09a244a8, 0x2d20159e, 0x40a6e6c4, 0x6424b7f2, 0x9bab0070, 0xbf295146, 0xd2afa21c, 0xf62df32a, 0xd377d2fa, 0xf7f583cc, 0x9a737096, 0xbef121a0, 0x417e9622, 0x65fcc714, 0x087a344e, 0x2cf86578, 0x2c145d0b, 0x08960c3d, 0x6510ff67, 0x4192ae51, 0xbe1d19d3, 0x9a9f48e5, 0xf719bbbf, 0xd39bea89, 0xbdadf81f, 0x992fa929, 0xf4a95a73, 0xd02b0b45, 0x2fa4bcc7, 0x0b26edf1, 0x66a01eab, 0x42224f9d, 0x42ce77ee, 0x664c26d8, 0x0bcad582, 0x2f4884b4, 0xd0c73336, 0xf4456200, 0x99c3915a, 0xbd41c06c, 0x981be1bc, 0xbc99b08a, 0xd11f43d0, 0xf59d12e6, 0x0a12a564, 0x2e90f452, 0x43160708, 0x6794563e, 0x67786e4d, 0x43fa3f7b, 0x2e7ccc21, 0x0afe9d17, 0xf5712a95, 0xd1f37ba3, 0xbc7588f9, 0x98f7d9cf, 0x6019add5, 0x449bfce3, 0x291d0fb9, 0x0d9f5e8f, 0xf210e90d, 0xd692b83b, 0xbb144b61, 0x9f961a57, 0x9f7a2224, 0xbbf87312, 0xd67e8048, 0xf2fcd17e, 0x0d7366fc, 0x29f137ca, 0x4477c490, 0x60f595a6, 0x45afb476, 0x612de540, 0x0cab161a, 0x2829472c, 0xd7a6f0ae, 0xf324a198, 0x9ea252c2, 0xba2003f4, 0xbacc3b87, 0x9e4e6ab1, 0xf3c899eb, 0xd74ac8dd, 0x28c57f5f, 0x0c472e69, 0x61c1dd33, 0x45438c05, 0x2b759e93, 0x0ff7cfa5, 0x62713cff, 0x46f36dc9, 0xb97cda4b, 0x9dfe8b7d, 0xf0787827, 0xd4fa2911, 0xd4161162, 0xf0944054, 0x9d12b30e, 0xb990e238, 0x461f55ba, 0x629d048c, 0x0f1bf7d6, 0x2b99a6e0, 0x0ec38730, 0x2a41d606, 0x47c7255c, 0x6345746a, 0x9ccac3e8, 0xb84892de, 0xd5ce6184, 0xf14c30b2, 0xf1a008c1, 0xd52259f7, 0xb8a4aaad, 0x9c26fb9b, 0x63a94c19, 0x472b1d2f, 0x2aadee75, 0x0e2fbf43},
             new uint[]{0x00000000, 0x36f290f3, 0x6de521e6, 0x5b17b115, 0xdbca43cc, 0xed38d33f, 0xb62f622a, 0x80ddf2d9, 0x6ce581d9, 0x5a17112a, 0x0100a03f, 0x37f230cc, 0xb72fc215, 0x81dd52e6, 0xdacae3f3, 0xec387300, 0xd9cb03b2, 0xef399341, 0xb42e2254, 0x82dcb2a7, 0x0201407e, 0x34f3d08d, 0x6fe46198, 0x5916f16b, 0xb52e826b, 0x83dc1298, 0xd8cba38d, 0xee39337e, 0x6ee4c1a7, 0x58165154, 0x0301e041, 0x35f370b2, 0x68e70125, 0x5e1591d6, 0x050220c3, 0x33f0b030, 0xb32d42e9, 0x85dfd21a, 0xdec8630f, 0xe83af3fc, 0x040280fc, 0x32f0100f, 0x69e7a11a, 0x5f1531e9, 0xdfc8c330, 0xe93a53c3, 0xb22de2d6, 0x84df7225, 0xb12c0297, 0x87de9264, 0xdcc92371, 0xea3bb382, 0x6ae6415b, 0x5c14d1a8, 0x070360bd, 0x31f1f04e, 0xddc9834e, 0xeb3b13bd, 0xb02ca2a8, 0x86de325b, 0x0603c082, 0x30f15071, 0x6be6e164, 0x5d147197, 0xd1ce024a, 0xe73c92b9, 0xbc2b23ac, 0x8ad9b35f, 0x0a044186, 0x3cf6d175, 0x67e16060, 0x5113f093, 0xbd2b8393, 0x8bd91360, 0xd0cea275, 0xe63c3286, 0x66e1c05f, 0x501350ac, 0x0b04e1b9, 0x3df6714a, 0x080501f8, 0x3ef7910b, 0x65e0201e, 0x5312b0ed, 0xd3cf4234, 0xe53dd2c7, 0xbe2a63d2, 0x88d8f321, 0x64e08021, 0x521210d2, 0x0905a1c7, 0x3ff73134, 0xbf2ac3ed, 0x89d8531e, 0xd2cfe20b, 0xe43d72f8, 0xb929036f, 0x8fdb939c, 0xd4cc2289, 0xe23eb27a, 0x62e340a3, 0x5411d050, 0x0f066145, 0x39f4f1b6, 0xd5cc82b6, 0xe33e1245, 0xb829a350, 0x8edb33a3, 0x0e06c17a, 0x38f45189, 0x63e3e09c, 0x5511706f, 0x60e200dd, 0x5610902e, 0x0d07213b, 0x3bf5b1c8, 0xbb284311, 0x8ddad3e2, 0xd6cd62f7, 0xe03ff204, 0x0c078104, 0x3af511f7, 0x61e2a0e2, 0x57103011, 0xd7cdc2c8, 0xe13f523b, 0xba28e32e, 0x8cda73dd, 0x78ed02d5, 0x4e1f9226, 0x15082333, 0x23fab3c0, 0xa3274119, 0x95d5d1ea, 0xcec260ff, 0xf830f00c, 0x1408830c, 0x22fa13ff, 0x79eda2ea, 0x4f1f3219, 0xcfc2c0c0, 0xf9305033, 0xa227e126, 0x94d571d5, 0xa1260167, 0x97d49194, 0xccc32081, 0xfa31b072, 0x7aec42ab, 0x4c1ed258, 0x1709634d, 0x21fbf3be, 0xcdc380be, 0xfb31104d, 0xa026a158, 0x96d431ab, 0x1609c372, 0x20fb5381, 0x7bece294, 0x4d1e7267, 0x100a03f0, 0x26f89303, 0x7def2216, 0x4b1db2e5, 0xcbc0403c, 0xfd32d0cf, 0xa62561da, 0x90d7f129, 0x7cef8229, 0x4a1d12da, 0x110aa3cf, 0x27f8333c, 0xa725c1e5, 0x91d75116, 0xcac0e003, 0xfc3270f0, 0xc9c10042, 0xff3390b1, 0xa42421a4, 0x92d6b157, 0x120b438e, 0x24f9d37d, 0x7fee6268, 0x491cf29b, 0xa524819b, 0x93d61168, 0xc8c1a07d, 0xfe33308e, 0x7eeec257, 0x481c52a4, 0x130be3b1, 0x25f97342, 0xa923009f, 0x9fd1906c, 0xc4c62179, 0xf234b18a, 0x72e94353, 0x441bd3a0, 0x1f0c62b5, 0x29fef246, 0xc5c68146, 0xf33411b5, 0xa823a0a0, 0x9ed13053, 0x1e0cc28a, 0x28fe5279, 0x73e9e36c, 0x451b739f, 0x70e8032d, 0x461a93de, 0x1d0d22cb, 0x2bffb238, 0xab2240e1, 0x9dd0d012, 0xc6c76107, 0xf035f1f4, 0x1c0d82f4, 0x2aff1207, 0x71e8a312, 0x471a33e1, 0xc7c7c138, 0xf13551cb, 0xaa22e0de, 0x9cd0702d, 0xc1c401ba, 0xf7369149, 0xac21205c, 0x9ad3b0af, 0x1a0e4276, 0x2cfcd285, 0x77eb6390, 0x4119f363, 0xad218063, 0x9bd31090, 0xc0c4a185, 0xf6363176, 0x76ebc3af, 0x4019535c, 0x1b0ee249, 0x2dfc72ba, 0x180f0208, 0x2efd92fb, 0x75ea23ee, 0x4318b31d, 0xc3c541c4, 0xf537d137, 0xae206022, 0x98d2f0d1, 0x74ea83d1, 0x42181322, 0x190fa237, 0x2ffd32c4, 0xaf20c01d, 0x99d250ee, 0xc2c5e1fb, 0xf4377108}
        };
        public static ulong[][] crc_braid_big_table = new ulong[][]
        {
            new ulong[]{0x0000000000000000, 0xf390f23600000000, 0xe621e56d00000000, 0x15b1175b00000000, 0xcc43cadb00000000, 0x3fd338ed00000000, 0x2a622fb600000000, 0xd9f2dd8000000000, 0xd981e56c00000000, 0x2a11175a00000000, 0x3fa0000100000000, 0xcc30f23700000000, 0x15c22fb700000000, 0xe652dd8100000000, 0xf3e3cada00000000, 0x007338ec00000000, 0xb203cbd900000000, 0x419339ef00000000, 0x54222eb400000000, 0xa7b2dc8200000000, 0x7e40010200000000, 0x8dd0f33400000000, 0x9861e46f00000000, 0x6bf1165900000000, 0x6b822eb500000000, 0x9812dc8300000000, 0x8da3cbd800000000, 0x7e3339ee00000000, 0xa7c1e46e00000000, 0x5451165800000000, 0x41e0010300000000, 0xb270f33500000000, 0x2501e76800000000, 0xd691155e00000000, 0xc320020500000000, 0x30b0f03300000000, 0xe9422db300000000, 0x1ad2df8500000000, 0x0f63c8de00000000, 0xfcf33ae800000000, 0xfc80020400000000, 0x0f10f03200000000, 0x1aa1e76900000000, 0xe931155f00000000, 0x30c3c8df00000000, 0xc3533ae900000000, 0xd6e22db200000000, 0x2572df8400000000, 0x97022cb100000000, 0x6492de8700000000, 0x7123c9dc00000000, 0x82b33bea00000000, 0x5b41e66a00000000, 0xa8d1145c00000000, 0xbd60030700000000, 0x4ef0f13100000000, 0x4e83c9dd00000000, 0xbd133beb00000000, 0xa8a22cb000000000, 0x5b32de8600000000, 0x82c0030600000000, 0x7150f13000000000, 0x64e1e66b00000000, 0x9771145d00000000, 0x4a02ced100000000, 0xb9923ce700000000, 0xac232bbc00000000, 0x5fb3d98a00000000, 0x8641040a00000000, 0x75d1f63c00000000, 0x6060e16700000000, 0x93f0135100000000, 0x93832bbd00000000, 0x6013d98b00000000, 0x75a2ced000000000, 0x86323ce600000000, 0x5fc0e16600000000, 0xac50135000000000, 0xb9e1040b00000000, 0x4a71f63d00000000, 0xf801050800000000, 0x0b91f73e00000000, 0x1e20e06500000000, 0xedb0125300000000, 0x3442cfd300000000, 0xc7d23de500000000, 0xd2632abe00000000, 0x21f3d88800000000, 0x2180e06400000000, 0xd210125200000000, 0xc7a1050900000000, 0x3431f73f00000000, 0xedc32abf00000000, 0x1e53d88900000000, 0x0be2cfd200000000, 0xf8723de400000000, 0x6f0329b900000000, 0x9c93db8f00000000, 0x8922ccd400000000, 0x7ab23ee200000000, 0xa340e36200000000, 0x50d0115400000000, 0x4561060f00000000, 0xb6f1f43900000000, 0xb682ccd500000000, 0x45123ee300000000, 0x50a329b800000000, 0xa333db8e00000000, 0x7ac1060e00000000, 0x8951f43800000000, 0x9ce0e36300000000, 0x6f70115500000000, 0xdd00e26000000000, 0x2e90105600000000, 0x3b21070d00000000, 0xc8b1f53b00000000, 0x114328bb00000000, 0xe2d3da8d00000000, 0xf762cdd600000000, 0x04f23fe000000000, 0x0481070c00000000, 0xf711f53a00000000, 0xe2a0e26100000000, 0x1130105700000000, 0xc8c2cdd700000000, 0x3b523fe100000000, 0x2ee328ba00000000, 0xdd73da8c00000000, 0xd502ed7800000000, 0x26921f4e00000000, 0x3323081500000000, 0xc0b3fa2300000000, 0x194127a300000000, 0xead1d59500000000, 0xff60c2ce00000000, 0x0cf030f800000000, 0x0c83081400000000, 0xff13fa2200000000, 0xeaa2ed7900000000, 0x19321f4f00000000, 0xc0c0c2cf00000000, 0x335030f900000000, 0x26e127a200000000, 0xd571d59400000000, 0x670126a100000000, 0x9491d49700000000, 0x8120c3cc00000000, 0x72b031fa00000000, 0xab42ec7a00000000, 0x58d21e4c00000000, 0x4d63091700000000, 0xbef3fb2100000000, 0xbe80c3cd00000000, 0x4d1031fb00000000, 0x58a126a000000000, 0xab31d49600000000, 0x72c3091600000000, 0x8153fb2000000000, 0x94e2ec7b00000000, 0x67721e4d00000000, 0xf0030a1000000000, 0x0393f82600000000, 0x1622ef7d00000000, 0xe5b21d4b00000000, 0x3c40c0cb00000000, 0xcfd032fd00000000, 0xda6125a600000000, 0x29f1d79000000000, 0x2982ef7c00000000, 0xda121d4a00000000, 0xcfa30a1100000000, 0x3c33f82700000000, 0xe5c125a700000000, 0x1651d79100000000, 0x03e0c0ca00000000, 0xf07032fc00000000, 0x4200c1c900000000, 0xb19033ff00000000, 0xa42124a400000000, 0x57b1d69200000000, 0x8e430b1200000000, 0x7dd3f92400000000, 0x6862ee7f00000000, 0x9bf21c4900000000, 0x9b8124a500000000, 0x6811d69300000000, 0x7da0c1c800000000, 0x8e3033fe00000000, 0x57c2ee7e00000000, 0xa4521c4800000000, 0xb1e30b1300000000, 0x4273f92500000000, 0x9f0023a900000000, 0x6c90d19f00000000, 0x7921c6c400000000, 0x8ab134f200000000, 0x5343e97200000000, 0xa0d31b4400000000, 0xb5620c1f00000000, 0x46f2fe2900000000, 0x4681c6c500000000, 0xb51134f300000000, 0xa0a023a800000000, 0x5330d19e00000000, 0x8ac20c1e00000000, 0x7952fe2800000000, 0x6ce3e97300000000, 0x9f731b4500000000, 0x2d03e87000000000, 0xde931a4600000000, 0xcb220d1d00000000, 0x38b2ff2b00000000, 0xe14022ab00000000, 0x12d0d09d00000000, 0x0761c7c600000000, 0xf4f135f000000000, 0xf4820d1c00000000, 0x0712ff2a00000000, 0x12a3e87100000000, 0xe1331a4700000000, 0x38c1c7c700000000, 0xcb5135f100000000, 0xdee022aa00000000, 0x2d70d09c00000000, 0xba01c4c100000000, 0x499136f700000000, 0x5c2021ac00000000, 0xafb0d39a00000000, 0x76420e1a00000000, 0x85d2fc2c00000000, 0x9063eb7700000000, 0x63f3194100000000, 0x638021ad00000000, 0x9010d39b00000000, 0x85a1c4c000000000, 0x763136f600000000, 0xafc3eb7600000000, 0x5c53194000000000, 0x49e20e1b00000000, 0xba72fc2d00000000, 0x08020f1800000000, 0xfb92fd2e00000000, 0xee23ea7500000000, 0x1db3184300000000, 0xc441c5c300000000, 0x37d137f500000000, 0x226020ae00000000, 0xd1f0d29800000000, 0xd183ea7400000000, 0x2213184200000000, 0x37a20f1900000000, 0xc432fd2f00000000, 0x1dc020af00000000, 0xee50d29900000000, 0xfbe1c5c200000000, 0x087137f400000000},
            new ulong[]{0x0000000000000000, 0x3651822400000000, 0x6ca2044900000000, 0x5af3866d00000000, 0xd844099200000000, 0xee158bb600000000, 0xb4e60ddb00000000, 0x82b78fff00000000, 0xf18f63ff00000000, 0xc7dee1db00000000, 0x9d2d67b600000000, 0xab7ce59200000000, 0x29cb6a6d00000000, 0x1f9ae84900000000, 0x45696e2400000000, 0x7338ec0000000000, 0xa319b62500000000, 0x9548340100000000, 0xcfbbb26c00000000, 0xf9ea304800000000, 0x7b5dbfb700000000, 0x4d0c3d9300000000, 0x17ffbbfe00000000, 0x21ae39da00000000, 0x5296d5da00000000, 0x64c757fe00000000, 0x3e34d19300000000, 0x086553b700000000, 0x8ad2dc4800000000, 0xbc835e6c00000000, 0xe670d80100000000, 0xd0215a2500000000, 0x46336c4b00000000, 0x7062ee6f00000000, 0x2a91680200000000, 0x1cc0ea2600000000, 0x9e7765d900000000, 0xa826e7fd00000000, 0xf2d5619000000000, 0xc484e3b400000000, 0xb7bc0fb400000000, 0x81ed8d9000000000, 0xdb1e0bfd00000000, 0xed4f89d900000000, 0x6ff8062600000000, 0x59a9840200000000, 0x035a026f00000000, 0x350b804b00000000, 0xe52ada6e00000000, 0xd37b584a00000000, 0x8988de2700000000, 0xbfd95c0300000000, 0x3d6ed3fc00000000, 0x0b3f51d800000000, 0x51ccd7b500000000, 0x679d559100000000, 0x14a5b99100000000, 0x22f43bb500000000, 0x7807bdd800000000, 0x4e563ffc00000000, 0xcce1b00300000000, 0xfab0322700000000, 0xa043b44a00000000, 0x9612366e00000000, 0x8c66d89600000000, 0xba375ab200000000, 0xe0c4dcdf00000000, 0xd6955efb00000000, 0x5422d10400000000, 0x6273532000000000, 0x3880d54d00000000, 0x0ed1576900000000, 0x7de9bb6900000000, 0x4bb8394d00000000, 0x114bbf2000000000, 0x271a3d0400000000, 0xa5adb2fb00000000, 0x93fc30df00000000, 0xc90fb6b200000000, 0xff5e349600000000, 0x2f7f6eb300000000, 0x192eec9700000000, 0x43dd6afa00000000, 0x758ce8de00000000, 0xf73b672100000000, 0xc16ae50500000000, 0x9b99636800000000, 0xadc8e14c00000000, 0xdef00d4c00000000, 0xe8a18f6800000000, 0xb252090500000000, 0x84038b2100000000, 0x06b404de00000000, 0x30e586fa00000000, 0x6a16009700000000, 0x5c4782b300000000, 0xca55b4dd00000000, 0xfc0436f900000000, 0xa6f7b09400000000, 0x90a632b000000000, 0x1211bd4f00000000, 0x24403f6b00000000, 0x7eb3b90600000000, 0x48e23b2200000000, 0x3bdad72200000000, 0x0d8b550600000000, 0x5778d36b00000000, 0x6129514f00000000, 0xe39edeb000000000, 0xd5cf5c9400000000, 0x8f3cdaf900000000, 0xb96d58dd00000000, 0x694c02f800000000, 0x5f1d80dc00000000, 0x05ee06b100000000, 0x33bf849500000000, 0xb1080b6a00000000, 0x8759894e00000000, 0xddaa0f2300000000, 0xebfb8d0700000000, 0x98c3610700000000, 0xae92e32300000000, 0xf461654e00000000, 0xc230e76a00000000, 0x4087689500000000, 0x76d6eab100000000, 0x2c256cdc00000000, 0x1a74eef800000000, 0x59cbc1f600000000, 0x6f9a43d200000000, 0x3569c5bf00000000, 0x0338479b00000000, 0x818fc86400000000, 0xb7de4a4000000000, 0xed2dcc2d00000000, 0xdb7c4e0900000000, 0xa844a20900000000, 0x9e15202d00000000, 0xc4e6a64000000000, 0xf2b7246400000000, 0x7000ab9b00000000, 0x465129bf00000000, 0x1ca2afd200000000, 0x2af32df600000000, 0xfad277d300000000, 0xcc83f5f700000000, 0x9670739a00000000, 0xa021f1be00000000, 0x22967e4100000000, 0x14c7fc6500000000, 0x4e347a0800000000, 0x7865f82c00000000, 0x0b5d142c00000000, 0x3d0c960800000000, 0x67ff106500000000, 0x51ae924100000000, 0xd3191dbe00000000, 0xe5489f9a00000000, 0xbfbb19f700000000, 0x89ea9bd300000000, 0x1ff8adbd00000000, 0x29a92f9900000000, 0x735aa9f400000000, 0x450b2bd000000000, 0xc7bca42f00000000, 0xf1ed260b00000000, 0xab1ea06600000000, 0x9d4f224200000000, 0xee77ce4200000000, 0xd8264c6600000000, 0x82d5ca0b00000000, 0xb484482f00000000, 0x3633c7d000000000, 0x006245f400000000, 0x5a91c39900000000, 0x6cc041bd00000000, 0xbce11b9800000000, 0x8ab099bc00000000, 0xd0431fd100000000, 0xe6129df500000000, 0x64a5120a00000000, 0x52f4902e00000000, 0x0807164300000000, 0x3e56946700000000, 0x4d6e786700000000, 0x7b3ffa4300000000, 0x21cc7c2e00000000, 0x179dfe0a00000000, 0x952a71f500000000, 0xa37bf3d100000000, 0xf98875bc00000000, 0xcfd9f79800000000, 0xd5ad196000000000, 0xe3fc9b4400000000, 0xb90f1d2900000000, 0x8f5e9f0d00000000, 0x0de910f200000000, 0x3bb892d600000000, 0x614b14bb00000000, 0x571a969f00000000, 0x24227a9f00000000, 0x1273f8bb00000000, 0x48807ed600000000, 0x7ed1fcf200000000, 0xfc66730d00000000, 0xca37f12900000000, 0x90c4774400000000, 0xa695f56000000000, 0x76b4af4500000000, 0x40e52d6100000000, 0x1a16ab0c00000000, 0x2c47292800000000, 0xaef0a6d700000000, 0x98a124f300000000, 0xc252a29e00000000, 0xf40320ba00000000, 0x873bccba00000000, 0xb16a4e9e00000000, 0xeb99c8f300000000, 0xddc84ad700000000, 0x5f7fc52800000000, 0x692e470c00000000, 0x33ddc16100000000, 0x058c434500000000, 0x939e752b00000000, 0xa5cff70f00000000, 0xff3c716200000000, 0xc96df34600000000, 0x4bda7cb900000000, 0x7d8bfe9d00000000, 0x277878f000000000, 0x1129fad400000000, 0x621116d400000000, 0x544094f000000000, 0x0eb3129d00000000, 0x38e290b900000000, 0xba551f4600000000, 0x8c049d6200000000, 0xd6f71b0f00000000, 0xe0a6992b00000000, 0x3087c30e00000000, 0x06d6412a00000000, 0x5c25c74700000000, 0x6a74456300000000, 0xe8c3ca9c00000000, 0xde9248b800000000, 0x8461ced500000000, 0xb2304cf100000000, 0xc108a0f100000000, 0xf75922d500000000, 0xadaaa4b800000000, 0x9bfb269c00000000, 0x194ca96300000000, 0x2f1d2b4700000000, 0x75eead2a00000000, 0x43bf2f0e00000000},
            new ulong[]{0x0000000000000000, 0xc8179ecf00000000, 0xd1294d4400000000, 0x193ed38b00000000, 0xa2539a8800000000, 0x6a44044700000000, 0x737ad7cc00000000, 0xbb6d490300000000, 0x05a145ca00000000, 0xcdb6db0500000000, 0xd488088e00000000, 0x1c9f964100000000, 0xa7f2df4200000000, 0x6fe5418d00000000, 0x76db920600000000, 0xbecc0cc900000000, 0x4b44fa4f00000000, 0x8353648000000000, 0x9a6db70b00000000, 0x527a29c400000000, 0xe91760c700000000, 0x2100fe0800000000, 0x383e2d8300000000, 0xf029b34c00000000, 0x4ee5bf8500000000, 0x86f2214a00000000, 0x9fccf2c100000000, 0x57db6c0e00000000, 0xecb6250d00000000, 0x24a1bbc200000000, 0x3d9f684900000000, 0xf588f68600000000, 0x9688f49f00000000, 0x5e9f6a5000000000, 0x47a1b9db00000000, 0x8fb6271400000000, 0x34db6e1700000000, 0xfcccf0d800000000, 0xe5f2235300000000, 0x2de5bd9c00000000, 0x9329b15500000000, 0x5b3e2f9a00000000, 0x4200fc1100000000, 0x8a1762de00000000, 0x317a2bdd00000000, 0xf96db51200000000, 0xe053669900000000, 0x2844f85600000000, 0xddcc0ed000000000, 0x15db901f00000000, 0x0ce5439400000000, 0xc4f2dd5b00000000, 0x7f9f945800000000, 0xb7880a9700000000, 0xaeb6d91c00000000, 0x66a147d300000000, 0xd86d4b1a00000000, 0x107ad5d500000000, 0x0944065e00000000, 0xc153989100000000, 0x7a3ed19200000000, 0xb2294f5d00000000, 0xab179cd600000000, 0x6300021900000000, 0x6d1798e400000000, 0xa500062b00000000, 0xbc3ed5a000000000, 0x74294b6f00000000, 0xcf44026c00000000, 0x07539ca300000000, 0x1e6d4f2800000000, 0xd67ad1e700000000, 0x68b6dd2e00000000, 0xa0a143e100000000, 0xb99f906a00000000, 0x71880ea500000000, 0xcae547a600000000, 0x02f2d96900000000, 0x1bcc0ae200000000, 0xd3db942d00000000, 0x265362ab00000000, 0xee44fc6400000000, 0xf77a2fef00000000, 0x3f6db12000000000, 0x8400f82300000000, 0x4c1766ec00000000, 0x5529b56700000000, 0x9d3e2ba800000000, 0x23f2276100000000, 0xebe5b9ae00000000, 0xf2db6a2500000000, 0x3accf4ea00000000, 0x81a1bde900000000, 0x49b6232600000000, 0x5088f0ad00000000, 0x989f6e6200000000, 0xfb9f6c7b00000000, 0x3388f2b400000000, 0x2ab6213f00000000, 0xe2a1bff000000000, 0x59ccf6f300000000, 0x91db683c00000000, 0x88e5bbb700000000, 0x40f2257800000000, 0xfe3e29b100000000, 0x3629b77e00000000, 0x2f1764f500000000, 0xe700fa3a00000000, 0x5c6db33900000000, 0x947a2df600000000, 0x8d44fe7d00000000, 0x455360b200000000, 0xb0db963400000000, 0x78cc08fb00000000, 0x61f2db7000000000, 0xa9e545bf00000000, 0x12880cbc00000000, 0xda9f927300000000, 0xc3a141f800000000, 0x0bb6df3700000000, 0xb57ad3fe00000000, 0x7d6d4d3100000000, 0x64539eba00000000, 0xac44007500000000, 0x1729497600000000, 0xdf3ed7b900000000, 0xc600043200000000, 0x0e179afd00000000, 0x9b28411200000000, 0x533fdfdd00000000, 0x4a010c5600000000, 0x8216929900000000, 0x397bdb9a00000000, 0xf16c455500000000, 0xe85296de00000000, 0x2045081100000000, 0x9e8904d800000000, 0x569e9a1700000000, 0x4fa0499c00000000, 0x87b7d75300000000, 0x3cda9e5000000000, 0xf4cd009f00000000, 0xedf3d31400000000, 0x25e44ddb00000000, 0xd06cbb5d00000000, 0x187b259200000000, 0x0145f61900000000, 0xc95268d600000000, 0x723f21d500000000, 0xba28bf1a00000000, 0xa3166c9100000000, 0x6b01f25e00000000, 0xd5cdfe9700000000, 0x1dda605800000000, 0x04e4b3d300000000, 0xccf32d1c00000000, 0x779e641f00000000, 0xbf89fad000000000, 0xa6b7295b00000000, 0x6ea0b79400000000, 0x0da0b58d00000000, 0xc5b72b4200000000, 0xdc89f8c900000000, 0x149e660600000000, 0xaff32f0500000000, 0x67e4b1ca00000000, 0x7eda624100000000, 0xb6cdfc8e00000000, 0x0801f04700000000, 0xc0166e8800000000, 0xd928bd0300000000, 0x113f23cc00000000, 0xaa526acf00000000, 0x6245f40000000000, 0x7b7b278b00000000, 0xb36cb94400000000, 0x46e44fc200000000, 0x8ef3d10d00000000, 0x97cd028600000000, 0x5fda9c4900000000, 0xe4b7d54a00000000, 0x2ca04b8500000000, 0x359e980e00000000, 0xfd8906c100000000, 0x43450a0800000000, 0x8b5294c700000000, 0x926c474c00000000, 0x5a7bd98300000000, 0xe116908000000000, 0x29010e4f00000000, 0x303fddc400000000, 0xf828430b00000000, 0xf63fd9f600000000, 0x3e28473900000000, 0x271694b200000000, 0xef010a7d00000000, 0x546c437e00000000, 0x9c7bddb100000000, 0x85450e3a00000000, 0x4d5290f500000000, 0xf39e9c3c00000000, 0x3b8902f300000000, 0x22b7d17800000000, 0xeaa04fb700000000, 0x51cd06b400000000, 0x99da987b00000000, 0x80e44bf000000000, 0x48f3d53f00000000, 0xbd7b23b900000000, 0x756cbd7600000000, 0x6c526efd00000000, 0xa445f03200000000, 0x1f28b93100000000, 0xd73f27fe00000000, 0xce01f47500000000, 0x06166aba00000000, 0xb8da667300000000, 0x70cdf8bc00000000, 0x69f32b3700000000, 0xa1e4b5f800000000, 0x1a89fcfb00000000, 0xd29e623400000000, 0xcba0b1bf00000000, 0x03b72f7000000000, 0x60b72d6900000000, 0xa8a0b3a600000000, 0xb19e602d00000000, 0x7989fee200000000, 0xc2e4b7e100000000, 0x0af3292e00000000, 0x13cdfaa500000000, 0xdbda646a00000000, 0x651668a300000000, 0xad01f66c00000000, 0xb43f25e700000000, 0x7c28bb2800000000, 0xc745f22b00000000, 0x0f526ce400000000, 0x166cbf6f00000000, 0xde7b21a000000000, 0x2bf3d72600000000, 0xe3e449e900000000, 0xfada9a6200000000, 0x32cd04ad00000000, 0x89a04dae00000000, 0x41b7d36100000000, 0x588900ea00000000, 0x909e9e2500000000, 0x2e5292ec00000000, 0xe6450c2300000000, 0xff7bdfa800000000, 0x376c416700000000, 0x8c01086400000000, 0x441696ab00000000, 0x5d28452000000000, 0x953fdbef00000000},
            new ulong[]{0x0000000000000000, 0x95d4709500000000, 0x6baf90f100000000, 0xfe7be06400000000, 0x9758503800000000, 0x028c20ad00000000, 0xfcf7c0c900000000, 0x6923b05c00000000, 0x2eb1a07000000000, 0xbb65d0e500000000, 0x451e308100000000, 0xd0ca401400000000, 0xb9e9f04800000000, 0x2c3d80dd00000000, 0xd24660b900000000, 0x4792102c00000000, 0x5c6241e100000000, 0xc9b6317400000000, 0x37cdd11000000000, 0xa219a18500000000, 0xcb3a11d900000000, 0x5eee614c00000000, 0xa095812800000000, 0x3541f1bd00000000, 0x72d3e19100000000, 0xe707910400000000, 0x197c716000000000, 0x8ca801f500000000, 0xe58bb1a900000000, 0x705fc13c00000000, 0x8e24215800000000, 0x1bf051cd00000000, 0xf9c2f31900000000, 0x6c16838c00000000, 0x926d63e800000000, 0x07b9137d00000000, 0x6e9aa32100000000, 0xfb4ed3b400000000, 0x053533d000000000, 0x90e1434500000000, 0xd773536900000000, 0x42a723fc00000000, 0xbcdcc39800000000, 0x2908b30d00000000, 0x402b035100000000, 0xd5ff73c400000000, 0x2b8493a000000000, 0xbe50e33500000000, 0xa5a0b2f800000000, 0x3074c26d00000000, 0xce0f220900000000, 0x5bdb529c00000000, 0x32f8e2c000000000, 0xa72c925500000000, 0x5957723100000000, 0xcc8302a400000000, 0x8b11128800000000, 0x1ec5621d00000000, 0xe0be827900000000, 0x756af2ec00000000, 0x1c4942b000000000, 0x899d322500000000, 0x77e6d24100000000, 0xe232a2d400000000, 0xf285e73300000000, 0x675197a600000000, 0x992a77c200000000, 0x0cfe075700000000, 0x65ddb70b00000000, 0xf009c79e00000000, 0x0e7227fa00000000, 0x9ba6576f00000000, 0xdc34474300000000, 0x49e037d600000000, 0xb79bd7b200000000, 0x224fa72700000000, 0x4b6c177b00000000, 0xdeb867ee00000000, 0x20c3878a00000000, 0xb517f71f00000000, 0xaee7a6d200000000, 0x3b33d64700000000, 0xc548362300000000, 0x509c46b600000000, 0x39bff6ea00000000, 0xac6b867f00000000, 0x5210661b00000000, 0xc7c4168e00000000, 0x805606a200000000, 0x1582763700000000, 0xebf9965300000000, 0x7e2de6c600000000, 0x170e569a00000000, 0x82da260f00000000, 0x7ca1c66b00000000, 0xe975b6fe00000000, 0x0b47142a00000000, 0x9e9364bf00000000, 0x60e884db00000000, 0xf53cf44e00000000, 0x9c1f441200000000, 0x09cb348700000000, 0xf7b0d4e300000000, 0x6264a47600000000, 0x25f6b45a00000000, 0xb022c4cf00000000, 0x4e5924ab00000000, 0xdb8d543e00000000, 0xb2aee46200000000, 0x277a94f700000000, 0xd901749300000000, 0x4cd5040600000000, 0x572555cb00000000, 0xc2f1255e00000000, 0x3c8ac53a00000000, 0xa95eb5af00000000, 0xc07d05f300000000, 0x55a9756600000000, 0xabd2950200000000, 0x3e06e59700000000, 0x7994f5bb00000000, 0xec40852e00000000, 0x123b654a00000000, 0x87ef15df00000000, 0xeecca58300000000, 0x7b18d51600000000, 0x8563357200000000, 0x10b745e700000000, 0xe40bcf6700000000, 0x71dfbff200000000, 0x8fa45f9600000000, 0x1a702f0300000000, 0x73539f5f00000000, 0xe687efca00000000, 0x18fc0fae00000000, 0x8d287f3b00000000, 0xcaba6f1700000000, 0x5f6e1f8200000000, 0xa115ffe600000000, 0x34c18f7300000000, 0x5de23f2f00000000, 0xc8364fba00000000, 0x364dafde00000000, 0xa399df4b00000000, 0xb8698e8600000000, 0x2dbdfe1300000000, 0xd3c61e7700000000, 0x46126ee200000000, 0x2f31debe00000000, 0xbae5ae2b00000000, 0x449e4e4f00000000, 0xd14a3eda00000000, 0x96d82ef600000000, 0x030c5e6300000000, 0xfd77be0700000000, 0x68a3ce9200000000, 0x01807ece00000000, 0x94540e5b00000000, 0x6a2fee3f00000000, 0xfffb9eaa00000000, 0x1dc93c7e00000000, 0x881d4ceb00000000, 0x7666ac8f00000000, 0xe3b2dc1a00000000, 0x8a916c4600000000, 0x1f451cd300000000, 0xe13efcb700000000, 0x74ea8c2200000000, 0x33789c0e00000000, 0xa6acec9b00000000, 0x58d70cff00000000, 0xcd037c6a00000000, 0xa420cc3600000000, 0x31f4bca300000000, 0xcf8f5cc700000000, 0x5a5b2c5200000000, 0x41ab7d9f00000000, 0xd47f0d0a00000000, 0x2a04ed6e00000000, 0xbfd09dfb00000000, 0xd6f32da700000000, 0x43275d3200000000, 0xbd5cbd5600000000, 0x2888cdc300000000, 0x6f1addef00000000, 0xfacead7a00000000, 0x04b54d1e00000000, 0x91613d8b00000000, 0xf8428dd700000000, 0x6d96fd4200000000, 0x93ed1d2600000000, 0x06396db300000000, 0x168e285400000000, 0x835a58c100000000, 0x7d21b8a500000000, 0xe8f5c83000000000, 0x81d6786c00000000, 0x140208f900000000, 0xea79e89d00000000, 0x7fad980800000000, 0x383f882400000000, 0xadebf8b100000000, 0x539018d500000000, 0xc644684000000000, 0xaf67d81c00000000, 0x3ab3a88900000000, 0xc4c848ed00000000, 0x511c387800000000, 0x4aec69b500000000, 0xdf38192000000000, 0x2143f94400000000, 0xb49789d100000000, 0xddb4398d00000000, 0x4860491800000000, 0xb61ba97c00000000, 0x23cfd9e900000000, 0x645dc9c500000000, 0xf189b95000000000, 0x0ff2593400000000, 0x9a2629a100000000, 0xf30599fd00000000, 0x66d1e96800000000, 0x98aa090c00000000, 0x0d7e799900000000, 0xef4cdb4d00000000, 0x7a98abd800000000, 0x84e34bbc00000000, 0x11373b2900000000, 0x78148b7500000000, 0xedc0fbe000000000, 0x13bb1b8400000000, 0x866f6b1100000000, 0xc1fd7b3d00000000, 0x54290ba800000000, 0xaa52ebcc00000000, 0x3f869b5900000000, 0x56a52b0500000000, 0xc3715b9000000000, 0x3d0abbf400000000, 0xa8decb6100000000, 0xb32e9aac00000000, 0x26faea3900000000, 0xd8810a5d00000000, 0x4d557ac800000000, 0x2476ca9400000000, 0xb1a2ba0100000000, 0x4fd95a6500000000, 0xda0d2af000000000, 0x9d9f3adc00000000, 0x084b4a4900000000, 0xf630aa2d00000000, 0x63e4dab800000000, 0x0ac76ae400000000, 0x9f131a7100000000, 0x6168fa1500000000, 0xf4bc8a8000000000},
            new ulong[]{0x0000000000000000, 0x1f17f08000000000, 0x7f2891da00000000, 0x603f615a00000000, 0xbf56536e00000000, 0xa041a3ee00000000, 0xc07ec2b400000000, 0xdf69323400000000, 0x7eada6dc00000000, 0x61ba565c00000000, 0x0185370600000000, 0x1e92c78600000000, 0xc1fbf5b200000000, 0xdeec053200000000, 0xbed3646800000000, 0xa1c494e800000000, 0xbd5c3c6200000000, 0xa24bcce200000000, 0xc274adb800000000, 0xdd635d3800000000, 0x020a6f0c00000000, 0x1d1d9f8c00000000, 0x7d22fed600000000, 0x62350e5600000000, 0xc3f19abe00000000, 0xdce66a3e00000000, 0xbcd90b6400000000, 0xa3cefbe400000000, 0x7ca7c9d000000000, 0x63b0395000000000, 0x038f580a00000000, 0x1c98a88a00000000, 0x7ab978c400000000, 0x65ae884400000000, 0x0591e91e00000000, 0x1a86199e00000000, 0xc5ef2baa00000000, 0xdaf8db2a00000000, 0xbac7ba7000000000, 0xa5d04af000000000, 0x0414de1800000000, 0x1b032e9800000000, 0x7b3c4fc200000000, 0x642bbf4200000000, 0xbb428d7600000000, 0xa4557df600000000, 0xc46a1cac00000000, 0xdb7dec2c00000000, 0xc7e544a600000000, 0xd8f2b42600000000, 0xb8cdd57c00000000, 0xa7da25fc00000000, 0x78b317c800000000, 0x67a4e74800000000, 0x079b861200000000, 0x188c769200000000, 0xb948e27a00000000, 0xa65f12fa00000000, 0xc66073a000000000, 0xd977832000000000, 0x061eb11400000000, 0x1909419400000000, 0x793620ce00000000, 0x6621d04e00000000, 0xb574805300000000, 0xaa6370d300000000, 0xca5c118900000000, 0xd54be10900000000, 0x0a22d33d00000000, 0x153523bd00000000, 0x750a42e700000000, 0x6a1db26700000000, 0xcbd9268f00000000, 0xd4ced60f00000000, 0xb4f1b75500000000, 0xabe647d500000000, 0x748f75e100000000, 0x6b98856100000000, 0x0ba7e43b00000000, 0x14b014bb00000000, 0x0828bc3100000000, 0x173f4cb100000000, 0x77002deb00000000, 0x6817dd6b00000000, 0xb77eef5f00000000, 0xa8691fdf00000000, 0xc8567e8500000000, 0xd7418e0500000000, 0x76851aed00000000, 0x6992ea6d00000000, 0x09ad8b3700000000, 0x16ba7bb700000000, 0xc9d3498300000000, 0xd6c4b90300000000, 0xb6fbd85900000000, 0xa9ec28d900000000, 0xcfcdf89700000000, 0xd0da081700000000, 0xb0e5694d00000000, 0xaff299cd00000000, 0x709babf900000000, 0x6f8c5b7900000000, 0x0fb33a2300000000, 0x10a4caa300000000, 0xb1605e4b00000000, 0xae77aecb00000000, 0xce48cf9100000000, 0xd15f3f1100000000, 0x0e360d2500000000, 0x1121fda500000000, 0x711e9cff00000000, 0x6e096c7f00000000, 0x7291c4f500000000, 0x6d86347500000000, 0x0db9552f00000000, 0x12aea5af00000000, 0xcdc7979b00000000, 0xd2d0671b00000000, 0xb2ef064100000000, 0xadf8f6c100000000, 0x0c3c622900000000, 0x132b92a900000000, 0x7314f3f300000000, 0x6c03037300000000, 0xb36a314700000000, 0xac7dc1c700000000, 0xcc42a09d00000000, 0xd355501d00000000, 0x6ae900a700000000, 0x75fef02700000000, 0x15c1917d00000000, 0x0ad661fd00000000, 0xd5bf53c900000000, 0xcaa8a34900000000, 0xaa97c21300000000, 0xb580329300000000, 0x1444a67b00000000, 0x0b5356fb00000000, 0x6b6c37a100000000, 0x747bc72100000000, 0xab12f51500000000, 0xb405059500000000, 0xd43a64cf00000000, 0xcb2d944f00000000, 0xd7b53cc500000000, 0xc8a2cc4500000000, 0xa89dad1f00000000, 0xb78a5d9f00000000, 0x68e36fab00000000, 0x77f49f2b00000000, 0x17cbfe7100000000, 0x08dc0ef100000000, 0xa9189a1900000000, 0xb60f6a9900000000, 0xd6300bc300000000, 0xc927fb4300000000, 0x164ec97700000000, 0x095939f700000000, 0x696658ad00000000, 0x7671a82d00000000, 0x1050786300000000, 0x0f4788e300000000, 0x6f78e9b900000000, 0x706f193900000000, 0xaf062b0d00000000, 0xb011db8d00000000, 0xd02ebad700000000, 0xcf394a5700000000, 0x6efddebf00000000, 0x71ea2e3f00000000, 0x11d54f6500000000, 0x0ec2bfe500000000, 0xd1ab8dd100000000, 0xcebc7d5100000000, 0xae831c0b00000000, 0xb194ec8b00000000, 0xad0c440100000000, 0xb21bb48100000000, 0xd224d5db00000000, 0xcd33255b00000000, 0x125a176f00000000, 0x0d4de7ef00000000, 0x6d7286b500000000, 0x7265763500000000, 0xd3a1e2dd00000000, 0xccb6125d00000000, 0xac89730700000000, 0xb39e838700000000, 0x6cf7b1b300000000, 0x73e0413300000000, 0x13df206900000000, 0x0cc8d0e900000000, 0xdf9d80f400000000, 0xc08a707400000000, 0xa0b5112e00000000, 0xbfa2e1ae00000000, 0x60cbd39a00000000, 0x7fdc231a00000000, 0x1fe3424000000000, 0x00f4b2c000000000, 0xa130262800000000, 0xbe27d6a800000000, 0xde18b7f200000000, 0xc10f477200000000, 0x1e66754600000000, 0x017185c600000000, 0x614ee49c00000000, 0x7e59141c00000000, 0x62c1bc9600000000, 0x7dd64c1600000000, 0x1de92d4c00000000, 0x02feddcc00000000, 0xdd97eff800000000, 0xc2801f7800000000, 0xa2bf7e2200000000, 0xbda88ea200000000, 0x1c6c1a4a00000000, 0x037beaca00000000, 0x63448b9000000000, 0x7c537b1000000000, 0xa33a492400000000, 0xbc2db9a400000000, 0xdc12d8fe00000000, 0xc305287e00000000, 0xa524f83000000000, 0xba3308b000000000, 0xda0c69ea00000000, 0xc51b996a00000000, 0x1a72ab5e00000000, 0x05655bde00000000, 0x655a3a8400000000, 0x7a4dca0400000000, 0xdb895eec00000000, 0xc49eae6c00000000, 0xa4a1cf3600000000, 0xbbb63fb600000000, 0x64df0d8200000000, 0x7bc8fd0200000000, 0x1bf79c5800000000, 0x04e06cd800000000, 0x1878c45200000000, 0x076f34d200000000, 0x6750558800000000, 0x7847a50800000000, 0xa72e973c00000000, 0xb83967bc00000000, 0xd80606e600000000, 0xc711f66600000000, 0x66d5628e00000000, 0x79c2920e00000000, 0x19fdf35400000000, 0x06ea03d400000000, 0xd98331e000000000, 0xc694c16000000000, 0xa6aba03a00000000, 0xb9bc50ba00000000},
            new ulong[]{0x0000000000000000, 0xe2fd888d00000000, 0x85fd60c000000000, 0x6700e84d00000000, 0x4bfdb05b00000000, 0xa90038d600000000, 0xce00d09b00000000, 0x2cfd581600000000, 0x96fa61b700000000, 0x7407e93a00000000, 0x1307017700000000, 0xf1fa89fa00000000, 0xdd07d1ec00000000, 0x3ffa596100000000, 0x58fab12c00000000, 0xba0739a100000000, 0x6df3b2b500000000, 0x8f0e3a3800000000, 0xe80ed27500000000, 0x0af35af800000000, 0x260e02ee00000000, 0xc4f38a6300000000, 0xa3f3622e00000000, 0x410eeaa300000000, 0xfb09d30200000000, 0x19f45b8f00000000, 0x7ef4b3c200000000, 0x9c093b4f00000000, 0xb0f4635900000000, 0x5209ebd400000000, 0x3509039900000000, 0xd7f48b1400000000, 0x9be014b000000000, 0x791d9c3d00000000, 0x1e1d747000000000, 0xfce0fcfd00000000, 0xd01da4eb00000000, 0x32e02c6600000000, 0x55e0c42b00000000, 0xb71d4ca600000000, 0x0d1a750700000000, 0xefe7fd8a00000000, 0x88e715c700000000, 0x6a1a9d4a00000000, 0x46e7c55c00000000, 0xa41a4dd100000000, 0xc31aa59c00000000, 0x21e72d1100000000, 0xf613a60500000000, 0x14ee2e8800000000, 0x73eec6c500000000, 0x91134e4800000000, 0xbdee165e00000000, 0x5f139ed300000000, 0x3813769e00000000, 0xdaeefe1300000000, 0x60e9c7b200000000, 0x82144f3f00000000, 0xe514a77200000000, 0x07e92fff00000000, 0x2b1477e900000000, 0xc9e9ff6400000000, 0xaee9172900000000, 0x4c149fa400000000, 0x77c758bb00000000, 0x953ad03600000000, 0xf23a387b00000000, 0x10c7b0f600000000, 0x3c3ae8e000000000, 0xdec7606d00000000, 0xb9c7882000000000, 0x5b3a00ad00000000, 0xe13d390c00000000, 0x03c0b18100000000, 0x64c059cc00000000, 0x863dd14100000000, 0xaac0895700000000, 0x483d01da00000000, 0x2f3de99700000000, 0xcdc0611a00000000, 0x1a34ea0e00000000, 0xf8c9628300000000, 0x9fc98ace00000000, 0x7d34024300000000, 0x51c95a5500000000, 0xb334d2d800000000, 0xd4343a9500000000, 0x36c9b21800000000, 0x8cce8bb900000000, 0x6e33033400000000, 0x0933eb7900000000, 0xebce63f400000000, 0xc7333be200000000, 0x25ceb36f00000000, 0x42ce5b2200000000, 0xa033d3af00000000, 0xec274c0b00000000, 0x0edac48600000000, 0x69da2ccb00000000, 0x8b27a44600000000, 0xa7dafc5000000000, 0x452774dd00000000, 0x22279c9000000000, 0xc0da141d00000000, 0x7add2dbc00000000, 0x9820a53100000000, 0xff204d7c00000000, 0x1dddc5f100000000, 0x31209de700000000, 0xd3dd156a00000000, 0xb4ddfd2700000000, 0x562075aa00000000, 0x81d4febe00000000, 0x6329763300000000, 0x04299e7e00000000, 0xe6d416f300000000, 0xca294ee500000000, 0x28d4c66800000000, 0x4fd42e2500000000, 0xad29a6a800000000, 0x172e9f0900000000, 0xf5d3178400000000, 0x92d3ffc900000000, 0x702e774400000000, 0x5cd32f5200000000, 0xbe2ea7df00000000, 0xd92e4f9200000000, 0x3bd3c71f00000000, 0xaf88c0ad00000000, 0x4d75482000000000, 0x2a75a06d00000000, 0xc88828e000000000, 0xe47570f600000000, 0x0688f87b00000000, 0x6188103600000000, 0x837598bb00000000, 0x3972a11a00000000, 0xdb8f299700000000, 0xbc8fc1da00000000, 0x5e72495700000000, 0x728f114100000000, 0x907299cc00000000, 0xf772718100000000, 0x158ff90c00000000, 0xc27b721800000000, 0x2086fa9500000000, 0x478612d800000000, 0xa57b9a5500000000, 0x8986c24300000000, 0x6b7b4ace00000000, 0x0c7ba28300000000, 0xee862a0e00000000, 0x548113af00000000, 0xb67c9b2200000000, 0xd17c736f00000000, 0x3381fbe200000000, 0x1f7ca3f400000000, 0xfd812b7900000000, 0x9a81c33400000000, 0x787c4bb900000000, 0x3468d41d00000000, 0xd6955c9000000000, 0xb195b4dd00000000, 0x53683c5000000000, 0x7f95644600000000, 0x9d68eccb00000000, 0xfa68048600000000, 0x18958c0b00000000, 0xa292b5aa00000000, 0x406f3d2700000000, 0x276fd56a00000000, 0xc5925de700000000, 0xe96f05f100000000, 0x0b928d7c00000000, 0x6c92653100000000, 0x8e6fedbc00000000, 0x599b66a800000000, 0xbb66ee2500000000, 0xdc66066800000000, 0x3e9b8ee500000000, 0x1266d6f300000000, 0xf09b5e7e00000000, 0x979bb63300000000, 0x75663ebe00000000, 0xcf61071f00000000, 0x2d9c8f9200000000, 0x4a9c67df00000000, 0xa861ef5200000000, 0x849cb74400000000, 0x66613fc900000000, 0x0161d78400000000, 0xe39c5f0900000000, 0xd84f981600000000, 0x3ab2109b00000000, 0x5db2f8d600000000, 0xbf4f705b00000000, 0x93b2284d00000000, 0x714fa0c000000000, 0x164f488d00000000, 0xf4b2c00000000000, 0x4eb5f9a100000000, 0xac48712c00000000, 0xcb48996100000000, 0x29b511ec00000000, 0x054849fa00000000, 0xe7b5c17700000000, 0x80b5293a00000000, 0x6248a1b700000000, 0xb5bc2aa300000000, 0x5741a22e00000000, 0x30414a6300000000, 0xd2bcc2ee00000000, 0xfe419af800000000, 0x1cbc127500000000, 0x7bbcfa3800000000, 0x994172b500000000, 0x23464b1400000000, 0xc1bbc39900000000, 0xa6bb2bd400000000, 0x4446a35900000000, 0x68bbfb4f00000000, 0x8a4673c200000000, 0xed469b8f00000000, 0x0fbb130200000000, 0x43af8ca600000000, 0xa152042b00000000, 0xc652ec6600000000, 0x24af64eb00000000, 0x08523cfd00000000, 0xeaafb47000000000, 0x8daf5c3d00000000, 0x6f52d4b000000000, 0xd555ed1100000000, 0x37a8659c00000000, 0x50a88dd100000000, 0xb255055c00000000, 0x9ea85d4a00000000, 0x7c55d5c700000000, 0x1b553d8a00000000, 0xf9a8b50700000000, 0x2e5c3e1300000000, 0xcca1b69e00000000, 0xaba15ed300000000, 0x495cd65e00000000, 0x65a18e4800000000, 0x875c06c500000000, 0xe05cee8800000000, 0x02a1660500000000, 0xb8a65fa400000000, 0x5a5bd72900000000, 0x3d5b3f6400000000, 0xdfa6b7e900000000, 0xf35befff00000000, 0x11a6677200000000, 0x76a68f3f00000000, 0x945b07b200000000},
            new ulong[]{0x0000000000000000, 0xa90b894e00000000, 0x5217129d00000000, 0xfb1c9bd300000000, 0xe52855e100000000, 0x4c23dcaf00000000, 0xb73f477c00000000, 0x1e34ce3200000000, 0x8b57db1900000000, 0x225c525700000000, 0xd940c98400000000, 0x704b40ca00000000, 0x6e7f8ef800000000, 0xc77407b600000000, 0x3c689c6500000000, 0x9563152b00000000, 0x16afb63300000000, 0xbfa43f7d00000000, 0x44b8a4ae00000000, 0xedb32de000000000, 0xf387e3d200000000, 0x5a8c6a9c00000000, 0xa190f14f00000000, 0x089b780100000000, 0x9df86d2a00000000, 0x34f3e46400000000, 0xcfef7fb700000000, 0x66e4f6f900000000, 0x78d038cb00000000, 0xd1dbb18500000000, 0x2ac72a5600000000, 0x83cca31800000000, 0x2c5e6d6700000000, 0x8555e42900000000, 0x7e497ffa00000000, 0xd742f6b400000000, 0xc976388600000000, 0x607db1c800000000, 0x9b612a1b00000000, 0x326aa35500000000, 0xa709b67e00000000, 0x0e023f3000000000, 0xf51ea4e300000000, 0x5c152dad00000000, 0x4221e39f00000000, 0xeb2a6ad100000000, 0x1036f10200000000, 0xb93d784c00000000, 0x3af1db5400000000, 0x93fa521a00000000, 0x68e6c9c900000000, 0xc1ed408700000000, 0xdfd98eb500000000, 0x76d207fb00000000, 0x8dce9c2800000000, 0x24c5156600000000, 0xb1a6004d00000000, 0x18ad890300000000, 0xe3b112d000000000, 0x4aba9b9e00000000, 0x548e55ac00000000, 0xfd85dce200000000, 0x0699473100000000, 0xaf92ce7f00000000, 0x58bcdace00000000, 0xf1b7538000000000, 0x0aabc85300000000, 0xa3a0411d00000000, 0xbd948f2f00000000, 0x149f066100000000, 0xef839db200000000, 0x468814fc00000000, 0xd3eb01d700000000, 0x7ae0889900000000, 0x81fc134a00000000, 0x28f79a0400000000, 0x36c3543600000000, 0x9fc8dd7800000000, 0x64d446ab00000000, 0xcddfcfe500000000, 0x4e136cfd00000000, 0xe718e5b300000000, 0x1c047e6000000000, 0xb50ff72e00000000, 0xab3b391c00000000, 0x0230b05200000000, 0xf92c2b8100000000, 0x5027a2cf00000000, 0xc544b7e400000000, 0x6c4f3eaa00000000, 0x9753a57900000000, 0x3e582c3700000000, 0x206ce20500000000, 0x89676b4b00000000, 0x727bf09800000000, 0xdb7079d600000000, 0x74e2b7a900000000, 0xdde93ee700000000, 0x26f5a53400000000, 0x8ffe2c7a00000000, 0x91cae24800000000, 0x38c16b0600000000, 0xc3ddf0d500000000, 0x6ad6799b00000000, 0xffb56cb000000000, 0x56bee5fe00000000, 0xada27e2d00000000, 0x04a9f76300000000, 0x1a9d395100000000, 0xb396b01f00000000, 0x488a2bcc00000000, 0xe181a28200000000, 0x624d019a00000000, 0xcb4688d400000000, 0x305a130700000000, 0x99519a4900000000, 0x8765547b00000000, 0x2e6edd3500000000, 0xd57246e600000000, 0x7c79cfa800000000, 0xe91ada8300000000, 0x401153cd00000000, 0xbb0dc81e00000000, 0x1206415000000000, 0x0c328f6200000000, 0xa539062c00000000, 0x5e259dff00000000, 0xf72e14b100000000, 0xf17ec44600000000, 0x58754d0800000000, 0xa369d6db00000000, 0x0a625f9500000000, 0x145691a700000000, 0xbd5d18e900000000, 0x4641833a00000000, 0xef4a0a7400000000, 0x7a291f5f00000000, 0xd322961100000000, 0x283e0dc200000000, 0x8135848c00000000, 0x9f014abe00000000, 0x360ac3f000000000, 0xcd16582300000000, 0x641dd16d00000000, 0xe7d1727500000000, 0x4edafb3b00000000, 0xb5c660e800000000, 0x1ccde9a600000000, 0x02f9279400000000, 0xabf2aeda00000000, 0x50ee350900000000, 0xf9e5bc4700000000, 0x6c86a96c00000000, 0xc58d202200000000, 0x3e91bbf100000000, 0x979a32bf00000000, 0x89aefc8d00000000, 0x20a575c300000000, 0xdbb9ee1000000000, 0x72b2675e00000000, 0xdd20a92100000000, 0x742b206f00000000, 0x8f37bbbc00000000, 0x263c32f200000000, 0x3808fcc000000000, 0x9103758e00000000, 0x6a1fee5d00000000, 0xc314671300000000, 0x5677723800000000, 0xff7cfb7600000000, 0x046060a500000000, 0xad6be9eb00000000, 0xb35f27d900000000, 0x1a54ae9700000000, 0xe148354400000000, 0x4843bc0a00000000, 0xcb8f1f1200000000, 0x6284965c00000000, 0x99980d8f00000000, 0x309384c100000000, 0x2ea74af300000000, 0x87acc3bd00000000, 0x7cb0586e00000000, 0xd5bbd12000000000, 0x40d8c40b00000000, 0xe9d34d4500000000, 0x12cfd69600000000, 0xbbc45fd800000000, 0xa5f091ea00000000, 0x0cfb18a400000000, 0xf7e7837700000000, 0x5eec0a3900000000, 0xa9c21e8800000000, 0x00c997c600000000, 0xfbd50c1500000000, 0x52de855b00000000, 0x4cea4b6900000000, 0xe5e1c22700000000, 0x1efd59f400000000, 0xb7f6d0ba00000000, 0x2295c59100000000, 0x8b9e4cdf00000000, 0x7082d70c00000000, 0xd9895e4200000000, 0xc7bd907000000000, 0x6eb6193e00000000, 0x95aa82ed00000000, 0x3ca10ba300000000, 0xbf6da8bb00000000, 0x166621f500000000, 0xed7aba2600000000, 0x4471336800000000, 0x5a45fd5a00000000, 0xf34e741400000000, 0x0852efc700000000, 0xa159668900000000, 0x343a73a200000000, 0x9d31faec00000000, 0x662d613f00000000, 0xcf26e87100000000, 0xd112264300000000, 0x7819af0d00000000, 0x830534de00000000, 0x2a0ebd9000000000, 0x859c73ef00000000, 0x2c97faa100000000, 0xd78b617200000000, 0x7e80e83c00000000, 0x60b4260e00000000, 0xc9bfaf4000000000, 0x32a3349300000000, 0x9ba8bddd00000000, 0x0ecba8f600000000, 0xa7c021b800000000, 0x5cdcba6b00000000, 0xf5d7332500000000, 0xebe3fd1700000000, 0x42e8745900000000, 0xb9f4ef8a00000000, 0x10ff66c400000000, 0x9333c5dc00000000, 0x3a384c9200000000, 0xc124d74100000000, 0x682f5e0f00000000, 0x761b903d00000000, 0xdf10197300000000, 0x240c82a000000000, 0x8d070bee00000000, 0x18641ec500000000, 0xb16f978b00000000, 0x4a730c5800000000, 0xe378851600000000, 0xfd4c4b2400000000, 0x5447c26a00000000, 0xaf5b59b900000000, 0x0650d0f700000000},
            new ulong[]{0x0000000000000000, 0x479244af00000000, 0xcf22f88500000000, 0x88b0bc2a00000000, 0xdf4381d000000000, 0x98d1c57f00000000, 0x1061795500000000, 0x57f33dfa00000000, 0xff81737a00000000, 0xb81337d500000000, 0x30a38bff00000000, 0x7731cf5000000000, 0x20c2f2aa00000000, 0x6750b60500000000, 0xefe00a2f00000000, 0xa8724e8000000000, 0xfe03e7f400000000, 0xb991a35b00000000, 0x31211f7100000000, 0x76b35bde00000000, 0x2140662400000000, 0x66d2228b00000000, 0xee629ea100000000, 0xa9f0da0e00000000, 0x0182948e00000000, 0x4610d02100000000, 0xcea06c0b00000000, 0x893228a400000000, 0xdec1155e00000000, 0x995351f100000000, 0x11e3eddb00000000, 0x5671a97400000000, 0xbd01bf3200000000, 0xfa93fb9d00000000, 0x722347b700000000, 0x35b1031800000000, 0x62423ee200000000, 0x25d07a4d00000000, 0xad60c66700000000, 0xeaf282c800000000, 0x4280cc4800000000, 0x051288e700000000, 0x8da234cd00000000, 0xca30706200000000, 0x9dc34d9800000000, 0xda51093700000000, 0x52e1b51d00000000, 0x1573f1b200000000, 0x430258c600000000, 0x04901c6900000000, 0x8c20a04300000000, 0xcbb2e4ec00000000, 0x9c41d91600000000, 0xdbd39db900000000, 0x5363219300000000, 0x14f1653c00000000, 0xbc832bbc00000000, 0xfb116f1300000000, 0x73a1d33900000000, 0x3433979600000000, 0x63c0aa6c00000000, 0x2452eec300000000, 0xace252e900000000, 0xeb70164600000000, 0x7a037e6500000000, 0x3d913aca00000000, 0xb52186e000000000, 0xf2b3c24f00000000, 0xa540ffb500000000, 0xe2d2bb1a00000000, 0x6a62073000000000, 0x2df0439f00000000, 0x85820d1f00000000, 0xc21049b000000000, 0x4aa0f59a00000000, 0x0d32b13500000000, 0x5ac18ccf00000000, 0x1d53c86000000000, 0x95e3744a00000000, 0xd27130e500000000, 0x8400999100000000, 0xc392dd3e00000000, 0x4b22611400000000, 0x0cb025bb00000000, 0x5b43184100000000, 0x1cd15cee00000000, 0x9461e0c400000000, 0xd3f3a46b00000000, 0x7b81eaeb00000000, 0x3c13ae4400000000, 0xb4a3126e00000000, 0xf33156c100000000, 0xa4c26b3b00000000, 0xe3502f9400000000, 0x6be093be00000000, 0x2c72d71100000000, 0xc702c15700000000, 0x809085f800000000, 0x082039d200000000, 0x4fb27d7d00000000, 0x1841408700000000, 0x5fd3042800000000, 0xd763b80200000000, 0x90f1fcad00000000, 0x3883b22d00000000, 0x7f11f68200000000, 0xf7a14aa800000000, 0xb0330e0700000000, 0xe7c033fd00000000, 0xa052775200000000, 0x28e2cb7800000000, 0x6f708fd700000000, 0x390126a300000000, 0x7e93620c00000000, 0xf623de2600000000, 0xb1b19a8900000000, 0xe642a77300000000, 0xa1d0e3dc00000000, 0x29605ff600000000, 0x6ef21b5900000000, 0xc68055d900000000, 0x8112117600000000, 0x09a2ad5c00000000, 0x4e30e9f300000000, 0x19c3d40900000000, 0x5e5190a600000000, 0xd6e12c8c00000000, 0x9173682300000000, 0xf406fcca00000000, 0xb394b86500000000, 0x3b24044f00000000, 0x7cb640e000000000, 0x2b457d1a00000000, 0x6cd739b500000000, 0xe467859f00000000, 0xa3f5c13000000000, 0x0b878fb000000000, 0x4c15cb1f00000000, 0xc4a5773500000000, 0x8337339a00000000, 0xd4c40e6000000000, 0x93564acf00000000, 0x1be6f6e500000000, 0x5c74b24a00000000, 0x0a051b3e00000000, 0x4d975f9100000000, 0xc527e3bb00000000, 0x82b5a71400000000, 0xd5469aee00000000, 0x92d4de4100000000, 0x1a64626b00000000, 0x5df626c400000000, 0xf584684400000000, 0xb2162ceb00000000, 0x3aa690c100000000, 0x7d34d46e00000000, 0x2ac7e99400000000, 0x6d55ad3b00000000, 0xe5e5111100000000, 0xa27755be00000000, 0x490743f800000000, 0x0e95075700000000, 0x8625bb7d00000000, 0xc1b7ffd200000000, 0x9644c22800000000, 0xd1d6868700000000, 0x59663aad00000000, 0x1ef47e0200000000, 0xb686308200000000, 0xf114742d00000000, 0x79a4c80700000000, 0x3e368ca800000000, 0x69c5b15200000000, 0x2e57f5fd00000000, 0xa6e749d700000000, 0xe1750d7800000000, 0xb704a40c00000000, 0xf096e0a300000000, 0x78265c8900000000, 0x3fb4182600000000, 0x684725dc00000000, 0x2fd5617300000000, 0xa765dd5900000000, 0xe0f799f600000000, 0x4885d77600000000, 0x0f1793d900000000, 0x87a72ff300000000, 0xc0356b5c00000000, 0x97c656a600000000, 0xd054120900000000, 0x58e4ae2300000000, 0x1f76ea8c00000000, 0x8e0582af00000000, 0xc997c60000000000, 0x41277a2a00000000, 0x06b53e8500000000, 0x5146037f00000000, 0x16d447d000000000, 0x9e64fbfa00000000, 0xd9f6bf5500000000, 0x7184f1d500000000, 0x3616b57a00000000, 0xbea6095000000000, 0xf9344dff00000000, 0xaec7700500000000, 0xe95534aa00000000, 0x61e5888000000000, 0x2677cc2f00000000, 0x7006655b00000000, 0x379421f400000000, 0xbf249dde00000000, 0xf8b6d97100000000, 0xaf45e48b00000000, 0xe8d7a02400000000, 0x60671c0e00000000, 0x27f558a100000000, 0x8f87162100000000, 0xc815528e00000000, 0x40a5eea400000000, 0x0737aa0b00000000, 0x50c497f100000000, 0x1756d35e00000000, 0x9fe66f7400000000, 0xd8742bdb00000000, 0x33043d9d00000000, 0x7496793200000000, 0xfc26c51800000000, 0xbbb481b700000000, 0xec47bc4d00000000, 0xabd5f8e200000000, 0x236544c800000000, 0x64f7006700000000, 0xcc854ee700000000, 0x8b170a4800000000, 0x03a7b66200000000, 0x4435f2cd00000000, 0x13c6cf3700000000, 0x54548b9800000000, 0xdce437b200000000, 0x9b76731d00000000, 0xcd07da6900000000, 0x8a959ec600000000, 0x022522ec00000000, 0x45b7664300000000, 0x12445bb900000000, 0x55d61f1600000000, 0xdd66a33c00000000, 0x9af4e79300000000, 0x3286a91300000000, 0x7514edbc00000000, 0xfda4519600000000, 0xba36153900000000, 0xedc528c300000000, 0xaa576c6c00000000, 0x22e7d04600000000, 0x657594e900000000}
        };
        public static uint[] x2n_table = new uint[]{0x40000000, 0x20000000, 0x08000000, 0x00800000, 0x00008000, 0xedb88320, 0xb1e6b092, 0xa06a2517, 0xed627dae, 0x88d14467, 0xd7bbfe6a, 0xec447f11, 0x8e7ea170, 0x6427800e, 0x4d47bae0, 0x09fe548f, 0x83852d0f, 0x30362f1a, 0x7b5a9cc3, 0x31fec169, 0x9fec022a, 0x6c8dedc4, 0x15d6874d, 0x5fde7a4e, 0xbad90e37, 0x2e4e5eef, 0x4eaba214, 0xa8a472c0, 0x429a969e, 0x148d302a, 0xc40ba6d0, 0xc4e22c3c};
        public static byte[] _length_code = new byte[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16, 16, 16, 17, 17, 17, 17, 17, 17, 17, 17, 18, 18, 18, 18, 18, 18, 18, 18, 19, 19, 19, 19, 19, 19, 19, 19, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28};
        public static byte[] _dist_code = new byte[]{0, 1, 2, 3, 4, 4, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 0, 0, 16, 17, 18, 18, 19, 19, 20, 20, 20, 20, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29};
        public static int[] extra_lbits = new int[]{0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0};
        public static int[] extra_dbits = new int[]{0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13};
        public static int[] extra_blbits = new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 7};
        public static byte[] bl_order = new byte[]{16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15};
        public static ct_data_s[] static_ltree = new ct_data_s[]
        {
            new ct_data_s() { fc = { code = 12 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 140 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 76 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 204 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 44 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 172 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 108 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 236 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 28 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 156 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 92 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 220 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 60 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 188 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 124 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 252 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 2 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 130 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 66 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 194 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 34 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 162 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 98 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 226 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 18 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 146 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 82 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 210 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 50 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 178 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 114 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 242 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 10 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 138 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 74 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 202 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 42 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 170 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 106 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 234 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 26 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 154 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 90 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 218 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 58 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 186 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 122 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 250 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 6 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 134 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 70 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 198 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 38 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 166 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 102 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 230 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 22 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 150 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 86 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 214 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 54 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 182 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 118 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 246 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 14 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 142 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 78 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 206 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 46 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 174 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 110 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 238 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 30 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 158 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 94 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 222 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 62 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 190 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 126 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 254 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 1 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 129 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 65 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 193 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 33 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 161 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 97 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 225 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 17 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 145 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 81 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 209 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 49 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 177 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 113 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 241 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 9 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 137 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 73 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 201 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 41 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 169 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 105 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 233 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 25 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 153 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 89 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 217 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 57 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 185 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 121 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 249 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 5 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 133 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 69 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 197 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 37 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 165 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 101 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 229 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 21 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 149 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 85 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 213 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 53 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 181 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 117 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 245 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 13 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 141 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 77 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 205 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 45 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 173 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 109 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 237 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 29 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 157 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 93 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 221 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 61 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 189 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 125 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 253 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 19 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 275 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 147 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 403 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 83 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 339 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 211 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 467 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 51 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 307 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 179 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 435 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 115 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 371 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 243 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 499 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 11 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 267 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 139 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 395 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 75 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 331 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 203 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 459 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 43 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 299 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 171 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 427 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 107 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 363 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 235 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 491 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 27 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 283 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 155 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 411 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 91 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 347 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 219 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 475 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 59 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 315 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 187 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 443 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 123 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 379 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 251 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 507 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 7 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 263 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 135 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 391 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 71 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 327 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 199 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 455 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 39 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 295 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 167 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 423 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 103 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 359 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 231 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 487 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 23 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 279 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 151 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 407 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 87 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 343 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 215 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 471 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 55 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 311 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 183 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 439 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 119 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 375 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 247 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 503 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 15 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 271 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 143 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 399 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 79 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 335 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 207 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 463 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 47 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 303 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 175 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 431 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 111 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 367 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 239 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 495 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 31 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 287 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 159 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 415 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 95 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 351 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 223 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 479 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 63 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 319 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 191 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 447 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 127 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 383 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 255 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 511 }, dl = { dad = 9 }},
            new ct_data_s() { fc = { code = 0 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 64 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 32 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 96 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 16 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 80 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 48 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 112 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 8 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 72 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 40 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 104 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 24 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 88 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 56 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 120 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 4 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 68 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 36 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 100 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 20 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 84 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 52 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 116 }, dl = { dad = 7 }},
            new ct_data_s() { fc = { code = 3 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 131 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 67 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 195 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 35 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 163 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 99 }, dl = { dad = 8 }},
            new ct_data_s() { fc = { code = 227 }, dl = { dad = 8 }}
        };

        public static ct_data_s[] static_dtree = new ct_data_s[]
        {
            new ct_data_s() { fc = { code = 0 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 16 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 8 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 24 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 4 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 20 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 12 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 28 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 2 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 18 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 10 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 26 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 6 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 22 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 14 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 30 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 1 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 17 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 9 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 25 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 5 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 21 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 13 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 29 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 3 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 19 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 11 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 27 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 7 }, dl = { dad = 5 }},
            new ct_data_s() { fc = { code = 23 }, dl = { dad = 5 }}
        };
        public static int[] base_length = new int[]{0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 14, 16, 20, 24, 28, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 0};
        public static int[] base_dist = new int[]{0, 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96, 128, 192, 256, 384, 512, 768, 1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384, 24576};

        public static static_tree_desc_s static_l_desc = new static_tree_desc_s { static_tree = static_ltree, extra_bits = extra_lbits, extra_base = 256 + 1, elems = (256 + 1 + 29), max_length = 15};
        public static static_tree_desc_s static_d_desc = new static_tree_desc_s { static_tree = static_dtree, extra_bits = extra_dbits, extra_base = 0, elems = 30, max_length = 15 };
        public static static_tree_desc_s static_bl_desc = new static_tree_desc_s { extra_bits = extra_blbits, extra_base = 0, elems = 19, max_length = 7 };
        public static sbyte[] deflate_copyright = new sbyte[69];
        public static config_s[] configuration_table = new config_s[]
        {
            /* 0 */ new config_s (0,    0,  0,    0, deflate_stored), /* store only */
            /* 1 */ new config_s (4,    4,  8,    4, deflate_fast), /* max speed, no lazy matches */
            /* 2 */ new config_s (4,    5, 16,    8, deflate_fast),
            /* 3 */ new config_s (4,    6, 32,   32, deflate_fast),
            /* 4 */ new config_s (4,    4, 16,   16, deflate_slow),  /* lazy matches */
            /* 5 */ new config_s (8,   16, 32,   32, deflate_slow),
            /* 6 */ new config_s (8,   16, 128, 128, deflate_slow),
            /* 7 */ new config_s (8,   32, 128, 256, deflate_slow),
            /* 8 */ new config_s (32, 128, 258, 1024, deflate_slow),
            /* 9 */ new config_s (32, 258, 258, 4096, deflate_slow) /* max compression */
    };

        public static sbyte[] inflate_copyright = new sbyte[48];
        public static code[] lenfix = new code[]
        {
                new code() { op = 96, bits = 7, val = 0 },
                new code() { op = 0, bits = 8, val = 80 },
                new code() { op = 0, bits = 8, val = 16 },
                new code() { op = 20, bits = 8, val = 115 },
                new code() { op = 18, bits = 7, val = 31 },
                new code() { op = 0, bits = 8, val = 112 },
                new code() { op = 0, bits = 8, val = 48 },
                new code() { op = 0, bits = 9, val = 192 },
                new code() { op = 16, bits = 7, val = 10 },
                new code() { op = 0, bits = 8, val = 96 },
                new code() { op = 0, bits = 8, val = 32 },
                new code() { op = 0, bits = 9, val = 160 },
                new code() { op = 0, bits = 8, val = 0 },
                new code() { op = 0, bits = 8, val = 128 },
                new code() { op = 0, bits = 8, val = 64 },
                new code() { op = 0, bits = 9, val = 224 },
                new code() { op = 16, bits = 7, val = 6 },
                new code() { op = 0, bits = 8, val = 88 },
                new code() { op = 0, bits = 8, val = 24 },
                new code() { op = 0, bits = 9, val = 144 },
                new code() { op = 19, bits = 7, val = 59 },
                new code() { op = 0, bits = 8, val = 120 },
                new code() { op = 0, bits = 8, val = 56 },
                new code() { op = 0, bits = 9, val = 208 },
                new code() { op = 17, bits = 7, val = 17 },
                new code() { op = 0, bits = 8, val = 104 },
                new code() { op = 0, bits = 8, val = 40 },
                new code() { op = 0, bits = 9, val = 176 },
                new code() { op = 0, bits = 8, val = 8 },
                new code() { op = 0, bits = 8, val = 136 },
                new code() { op = 0, bits = 8, val = 72 },
                new code() { op = 0, bits = 9, val = 240 },
                new code() { op = 16, bits = 7, val = 4 },
                new code() { op = 0, bits = 8, val = 84 },
                new code() { op = 0, bits = 8, val = 20 },
                new code() { op = 21, bits = 8, val = 227 },
                new code() { op = 19, bits = 7, val = 43 },
                new code() { op = 0, bits = 8, val = 116 },
                new code() { op = 0, bits = 8, val = 52 },
                new code() { op = 0, bits = 9, val = 200 },
                new code() { op = 17, bits = 7, val = 13 },
                new code() { op = 0, bits = 8, val = 100 },
                new code() { op = 0, bits = 8, val = 36 },
                new code() { op = 0, bits = 9, val = 168 },
                new code() { op = 0, bits = 8, val = 4 },
                new code() { op = 0, bits = 8, val = 132 },
                new code() { op = 0, bits = 8, val = 68 },
                new code() { op = 0, bits = 9, val = 232 },
                new code() { op = 16, bits = 7, val = 8 },
                new code() { op = 0, bits = 8, val = 92 },
                new code() { op = 0, bits = 8, val = 28 },
                new code() { op = 0, bits = 9, val = 152 },
                new code() { op = 20, bits = 7, val = 83 },
                new code() { op = 0, bits = 8, val = 124 },
                new code() { op = 0, bits = 8, val = 60 },
                new code() { op = 0, bits = 9, val = 216 },
                new code() { op = 18, bits = 7, val = 23 },
                new code() { op = 0, bits = 8, val = 108 },
                new code() { op = 0, bits = 8, val = 44 },
                new code() { op = 0, bits = 9, val = 184 },
                new code() { op = 0, bits = 8, val = 12 },
                new code() { op = 0, bits = 8, val = 140 },
                new code() { op = 0, bits = 8, val = 76 },
                new code() { op = 0, bits = 9, val = 248 },
                new code() { op = 16, bits = 7, val = 3 },
                new code() { op = 0, bits = 8, val = 82 },
                new code() { op = 0, bits = 8, val = 18 },
                new code() { op = 21, bits = 8, val = 163 },
                new code() { op = 19, bits = 7, val = 35 },
                new code() { op = 0, bits = 8, val = 114 },
                new code() { op = 0, bits = 8, val = 50 },
                new code() { op = 0, bits = 9, val = 196 },
                new code() { op = 17, bits = 7, val = 11 },
                new code() { op = 0, bits = 8, val = 98 },
                new code() { op = 0, bits = 8, val = 34 },
                new code() { op = 0, bits = 9, val = 164 },
                new code() { op = 0, bits = 8, val = 2 },
                new code() { op = 0, bits = 8, val = 130 },
                new code() { op = 0, bits = 8, val = 66 },
                new code() { op = 0, bits = 9, val = 228 },
                new code() { op = 16, bits = 7, val = 7 },
                new code() { op = 0, bits = 8, val = 90 },
                new code() { op = 0, bits = 8, val = 26 },
                new code() { op = 0, bits = 9, val = 148 },
                new code() { op = 20, bits = 7, val = 67 },
                new code() { op = 0, bits = 8, val = 122 },
                new code() { op = 0, bits = 8, val = 58 },
                new code() { op = 0, bits = 9, val = 212 },
                new code() { op = 18, bits = 7, val = 19 },
                new code() { op = 0, bits = 8, val = 106 },
                new code() { op = 0, bits = 8, val = 42 },
                new code() { op = 0, bits = 9, val = 180 },
                new code() { op = 0, bits = 8, val = 10 },
                new code() { op = 0, bits = 8, val = 138 },
                new code() { op = 0, bits = 8, val = 74 },
                new code() { op = 0, bits = 9, val = 244 },
                new code() { op = 16, bits = 7, val = 5 },
                new code() { op = 0, bits = 8, val = 86 },
                new code() { op = 0, bits = 8, val = 22 },
                new code() { op = 64, bits = 8, val = 0 },
                new code() { op = 19, bits = 7, val = 51 },
                new code() { op = 0, bits = 8, val = 118 },
                new code() { op = 0, bits = 8, val = 54 },
                new code() { op = 0, bits = 9, val = 204 },
                new code() { op = 17, bits = 7, val = 15 },
                new code() { op = 0, bits = 8, val = 102 },
                new code() { op = 0, bits = 8, val = 38 },
                new code() { op = 0, bits = 9, val = 172 },
                new code() { op = 0, bits = 8, val = 6 },
                new code() { op = 0, bits = 8, val = 134 },
                new code() { op = 0, bits = 8, val = 70 },
                new code() { op = 0, bits = 9, val = 236 },
                new code() { op = 16, bits = 7, val = 9 },
                new code() { op = 0, bits = 8, val = 94 },
                new code() { op = 0, bits = 8, val = 30 },
                new code() { op = 0, bits = 9, val = 156 },
                new code() { op = 20, bits = 7, val = 99 },
                new code() { op = 0, bits = 8, val = 126 },
                new code() { op = 0, bits = 8, val = 62 },
                new code() { op = 0, bits = 9, val = 220 },
                new code() { op = 18, bits = 7, val = 27 },
                new code() { op = 0, bits = 8, val = 110 },
                new code() { op = 0, bits = 8, val = 46 },
                new code() { op = 0, bits = 9, val = 188 },
                new code() { op = 0, bits = 8, val = 14 },
                new code() { op = 0, bits = 8, val = 142 },
                new code() { op = 0, bits = 8, val = 78 },
                new code() { op = 0, bits = 9, val = 252 },
                new code() { op = 96, bits = 7, val = 0 },
                new code() { op = 0, bits = 8, val = 81 },
                new code() { op = 0, bits = 8, val = 17 },
                new code() { op = 21, bits = 8, val = 131 },
                new code() { op = 18, bits = 7, val = 31 },
                new code() { op = 0, bits = 8, val = 113 },
                new code() { op = 0, bits = 8, val = 49 },
                new code() { op = 0, bits = 9, val = 194 },
                new code() { op = 16, bits = 7, val = 10 },
                new code() { op = 0, bits = 8, val = 97 },
                new code() { op = 0, bits = 8, val = 33 },
                new code() { op = 0, bits = 9, val = 162 },
                new code() { op = 0, bits = 8, val = 1 },
                new code() { op = 0, bits = 8, val = 129 },
                new code() { op = 0, bits = 8, val = 65 },
                new code() { op = 0, bits = 9, val = 226 },
                new code() { op = 16, bits = 7, val = 6 },
                new code() { op = 0, bits = 8, val = 89 },
                new code() { op = 0, bits = 8, val = 25 },
                new code() { op = 0, bits = 9, val = 146 },
                new code() { op = 19, bits = 7, val = 59 },
                new code() { op = 0, bits = 8, val = 121 },
                new code() { op = 0, bits = 8, val = 57 },
                new code() { op = 0, bits = 9, val = 210 },
                new code() { op = 17, bits = 7, val = 17 },
                new code() { op = 0, bits = 8, val = 105 },
                new code() { op = 0, bits = 8, val = 41 },
                new code() { op = 0, bits = 9, val = 178 },
                new code() { op = 0, bits = 8, val = 9 },
                new code() { op = 0, bits = 8, val = 137 },
                new code() { op = 0, bits = 8, val = 73 },
                new code() { op = 0, bits = 9, val = 242 },
                new code() { op = 16, bits = 7, val = 4 },
                new code() { op = 0, bits = 8, val = 85 },
                new code() { op = 0, bits = 8, val = 21 },
                new code() { op = 16, bits = 8, val = 258 },
                new code() { op = 19, bits = 7, val = 43 },
                new code() { op = 0, bits = 8, val = 117 },
                new code() { op = 0, bits = 8, val = 53 },
                new code() { op = 0, bits = 9, val = 202 },
                new code() { op = 17, bits = 7, val = 13 },
                new code() { op = 0, bits = 8, val = 101 },
                new code() { op = 0, bits = 8, val = 37 },
                new code() { op = 0, bits = 9, val = 170 },
                new code() { op = 0, bits = 8, val = 5 },
                new code() { op = 0, bits = 8, val = 133 },
                new code() { op = 0, bits = 8, val = 69 },
                new code() { op = 0, bits = 9, val = 234 },
                new code() { op = 16, bits = 7, val = 8 },
                new code() { op = 0, bits = 8, val = 93 },
                new code() { op = 0, bits = 8, val = 29 },
                new code() { op = 0, bits = 9, val = 154 },
                new code() { op = 20, bits = 7, val = 83 },
                new code() { op = 0, bits = 8, val = 125 },
                new code() { op = 0, bits = 8, val = 61 },
                new code() { op = 0, bits = 9, val = 218 },
                new code() { op = 18, bits = 7, val = 23 },
                new code() { op = 0, bits = 8, val = 109 },
                new code() { op = 0, bits = 8, val = 45 },
                new code() { op = 0, bits = 9, val = 186 },
                new code() { op = 0, bits = 8, val = 13 },
                new code() { op = 0, bits = 8, val = 141 },
                new code() { op = 0, bits = 8, val = 77 },
                new code() { op = 0, bits = 9, val = 250 },
                new code() { op = 16, bits = 7, val = 3 },
                new code() { op = 0, bits = 8, val = 83 },
                new code() { op = 0, bits = 8, val = 19 },
                new code() { op = 21, bits = 8, val = 195 },
                new code() { op = 19, bits = 7, val = 35 },
                new code() { op = 0, bits = 8, val = 115 },
                new code() { op = 0, bits = 8, val = 51 },
                new code() { op = 0, bits = 9, val = 198 },
                new code() { op = 17, bits = 7, val = 11 },
                new code() { op = 0, bits = 8, val = 99 },
                new code() { op = 0, bits = 8, val = 35 },
                new code() { op = 0, bits = 9, val = 166 },
                new code() { op = 0, bits = 8, val = 3 },
                new code() { op = 0, bits = 8, val = 131 },
                new code() { op = 0, bits = 8, val = 67 },
                new code() { op = 0, bits = 9, val = 230 },
                new code() { op = 16, bits = 7, val = 7 },
                new code() { op = 0, bits = 8, val = 91 },
                new code() { op = 0, bits = 8, val = 27 },
                new code() { op = 0, bits = 9, val = 150 },
                new code() { op = 20, bits = 7, val = 67 },
                new code() { op = 0, bits = 8, val = 123 },
                new code() { op = 0, bits = 8, val = 59 },
                new code() { op = 0, bits = 9, val = 214 },
                new code() { op = 18, bits = 7, val = 19 },
                new code() { op = 0, bits = 8, val = 107 },
                new code() { op = 0, bits = 8, val = 43 },
                new code() { op = 0, bits = 9, val = 182 },
                new code() { op = 0, bits = 8, val = 11 },
                new code() { op = 0, bits = 8, val = 139 },
                new code() { op = 0, bits = 8, val = 75 },
                new code() { op = 0, bits = 9, val = 246 },
                new code() { op = 16, bits = 7, val = 5 },
                new code() { op = 0, bits = 8, val = 87 },
                new code() { op = 0, bits = 8, val = 23 },
                new code() { op = 64, bits = 8, val = 0 },
                new code() { op = 19, bits = 7, val = 51 },
                new code() { op = 0, bits = 8, val = 119 },
                new code() { op = 0, bits = 8, val = 55 },
                new code() { op = 0, bits = 9, val = 206 },
                new code() { op = 17, bits = 7, val = 15 },
                new code() { op = 0, bits = 8, val = 103 },
                new code() { op = 0, bits = 8, val = 39 },
                new code() { op = 0, bits = 9, val = 174 },
                new code() { op = 0, bits = 8, val = 7 },
                new code() { op = 0, bits = 8, val = 135 },
                new code() { op = 0, bits = 8, val = 71 },
                new code() { op = 0, bits = 9, val = 238 },
                new code() { op = 16, bits = 7, val = 9 },
                new code() { op = 0, bits = 8, val = 95 },
                new code() { op = 0, bits = 8, val = 31 },
                new code() { op = 0, bits = 9, val = 158 },
                new code() { op = 20, bits = 7, val = 99 },
                new code() { op = 0, bits = 8, val = 127 },
                new code() { op = 0, bits = 8, val = 63 },
                new code() { op = 0, bits = 9, val = 222 },
                new code() { op = 18, bits = 7, val = 27 },
                new code() { op = 0, bits = 8, val = 111 },
                new code() { op = 0, bits = 8, val = 47 },
                new code() { op = 0, bits = 9, val = 190 },
                new code() { op = 0, bits = 8, val = 15 },
                new code() { op = 0, bits = 8, val = 143 },
                new code() { op = 0, bits = 8, val = 79 },
                new code() { op = 0, bits = 9, val = 254 },
                new code() { op = 96, bits = 7, val = 0 },
                new code() { op = 0, bits = 8, val = 80 },
                new code() { op = 0, bits = 8, val = 16 },
                new code() { op = 20, bits = 8, val = 115 },
                new code() { op = 18, bits = 7, val = 31 },
                new code() { op = 0, bits = 8, val = 112 },
                new code() { op = 0, bits = 8, val = 48 },
                new code() { op = 0, bits = 9, val = 193 },
                new code() { op = 16, bits = 7, val = 10 },
                new code() { op = 0, bits = 8, val = 96 },
                new code() { op = 0, bits = 8, val = 32 },
                new code() { op = 0, bits = 9, val = 161 },
                new code() { op = 0, bits = 8, val = 0 },
                new code() { op = 0, bits = 8, val = 128 },
                new code() { op = 0, bits = 8, val = 64 },
                new code() { op = 0, bits = 9, val = 225 },
                new code() { op = 16, bits = 7, val = 6 },
                new code() { op = 0, bits = 8, val = 88 },
                new code() { op = 0, bits = 8, val = 24 },
                new code() { op = 0, bits = 9, val = 145 },
                new code() { op = 19, bits = 7, val = 59 },
                new code() { op = 0, bits = 8, val = 120 },
                new code() { op = 0, bits = 8, val = 56 },
                new code() { op = 0, bits = 9, val = 209 },
                new code() { op = 17, bits = 7, val = 17 },
                new code() { op = 0, bits = 8, val = 104 },
                new code() { op = 0, bits = 8, val = 40 },
                new code() { op = 0, bits = 9, val = 177 },
                new code() { op = 0, bits = 8, val = 8 },
                new code() { op = 0, bits = 8, val = 136 },
                new code() { op = 0, bits = 8, val = 72 },
                new code() { op = 0, bits = 9, val = 241 },
                new code() { op = 16, bits = 7, val = 4 },
                new code() { op = 0, bits = 8, val = 84 },
                new code() { op = 0, bits = 8, val = 20 },
                new code() { op = 21, bits = 8, val = 227 },
                new code() { op = 19, bits = 7, val = 43 },
                new code() { op = 0, bits = 8, val = 116 },
                new code() { op = 0, bits = 8, val = 52 },
                new code() { op = 0, bits = 9, val = 201 },
                new code() { op = 17, bits = 7, val = 13 },
                new code() { op = 0, bits = 8, val = 100 },
                new code() { op = 0, bits = 8, val = 36 },
                new code() { op = 0, bits = 9, val = 169 },
                new code() { op = 0, bits = 8, val = 4 },
                new code() { op = 0, bits = 8, val = 132 },
                new code() { op = 0, bits = 8, val = 68 },
                new code() { op = 0, bits = 9, val = 233 },
                new code() { op = 16, bits = 7, val = 8 },
                new code() { op = 0, bits = 8, val = 92 },
                new code() { op = 0, bits = 8, val = 28 },
                new code() { op = 0, bits = 9, val = 153 },
                new code() { op = 20, bits = 7, val = 83 },
                new code() { op = 0, bits = 8, val = 124 },
                new code() { op = 0, bits = 8, val = 60 },
                new code() { op = 0, bits = 9, val = 217 },
                new code() { op = 18, bits = 7, val = 23 },
                new code() { op = 0, bits = 8, val = 108 },
                new code() { op = 0, bits = 8, val = 44 },
                new code() { op = 0, bits = 9, val = 185 },
                new code() { op = 0, bits = 8, val = 12 },
                new code() { op = 0, bits = 8, val = 140 },
                new code() { op = 0, bits = 8, val = 76 },
                new code() { op = 0, bits = 9, val = 249 },
                new code() { op = 16, bits = 7, val = 3 },
                new code() { op = 0, bits = 8, val = 82 },
                new code() { op = 0, bits = 8, val = 18 },
                new code() { op = 21, bits = 8, val = 163 },
                new code() { op = 19, bits = 7, val = 35 },
                new code() { op = 0, bits = 8, val = 114 },
                new code() { op = 0, bits = 8, val = 50 },
                new code() { op = 0, bits = 9, val = 197 },
                new code() { op = 17, bits = 7, val = 11 },
                new code() { op = 0, bits = 8, val = 98 },
                new code() { op = 0, bits = 8, val = 34 },
                new code() { op = 0, bits = 9, val = 165 },
                new code() { op = 0, bits = 8, val = 2 },
                new code() { op = 0, bits = 8, val = 130 },
                new code() { op = 0, bits = 8, val = 66 },
                new code() { op = 0, bits = 9, val = 229 },
                new code() { op = 16, bits = 7, val = 7 },
                new code() { op = 0, bits = 8, val = 90 },
                new code() { op = 0, bits = 8, val = 26 },
                new code() { op = 0, bits = 9, val = 149 },
                new code() { op = 20, bits = 7, val = 67 },
                new code() { op = 0, bits = 8, val = 122 },
                new code() { op = 0, bits = 8, val = 58 },
                new code() { op = 0, bits = 9, val = 213 },
                new code() { op = 18, bits = 7, val = 19 },
                new code() { op = 0, bits = 8, val = 106 },
                new code() { op = 0, bits = 8, val = 42 },
                new code() { op = 0, bits = 9, val = 181 },
                new code() { op = 0, bits = 8, val = 10 },
                new code() { op = 0, bits = 8, val = 138 },
                new code() { op = 0, bits = 8, val = 74 },
                new code() { op = 0, bits = 9, val = 245 },
                new code() { op = 16, bits = 7, val = 5 },
                new code() { op = 0, bits = 8, val = 86 },
                new code() { op = 0, bits = 8, val = 22 },
                new code() { op = 64, bits = 8, val = 0 },
                new code() { op = 19, bits = 7, val = 51 },
                new code() { op = 0, bits = 8, val = 118 },
                new code() { op = 0, bits = 8, val = 54 },
                new code() { op = 0, bits = 9, val = 205 },
                new code() { op = 17, bits = 7, val = 15 },
                new code() { op = 0, bits = 8, val = 102 },
                new code() { op = 0, bits = 8, val = 38 },
                new code() { op = 0, bits = 9, val = 173 },
                new code() { op = 0, bits = 8, val = 6 },
                new code() { op = 0, bits = 8, val = 134 },
                new code() { op = 0, bits = 8, val = 70 },
                new code() { op = 0, bits = 9, val = 237 },
                new code() { op = 16, bits = 7, val = 9 },
                new code() { op = 0, bits = 8, val = 94 },
                new code() { op = 0, bits = 8, val = 30 },
                new code() { op = 0, bits = 9, val = 157 },
                new code() { op = 20, bits = 7, val = 99 },
                new code() { op = 0, bits = 8, val = 126 },
                new code() { op = 0, bits = 8, val = 62 },
                new code() { op = 0, bits = 9, val = 221 },
                new code() { op = 18, bits = 7, val = 27 },
                new code() { op = 0, bits = 8, val = 110 },
                new code() { op = 0, bits = 8, val = 46 },
                new code() { op = 0, bits = 9, val = 189 },
                new code() { op = 0, bits = 8, val = 14 },
                new code() { op = 0, bits = 8, val = 142 },
                new code() { op = 0, bits = 8, val = 78 },
                new code() { op = 0, bits = 9, val = 253 },
                new code() { op = 96, bits = 7, val = 0 },
                new code() { op = 0, bits = 8, val = 81 },
                new code() { op = 0, bits = 8, val = 17 },
                new code() { op = 21, bits = 8, val = 131 },
                new code() { op = 18, bits = 7, val = 31 },
                new code() { op = 0, bits = 8, val = 113 },
                new code() { op = 0, bits = 8, val = 49 },
                new code() { op = 0, bits = 9, val = 195 },
                new code() { op = 16, bits = 7, val = 10 },
                new code() { op = 0, bits = 8, val = 97 },
                new code() { op = 0, bits = 8, val = 33 },
                new code() { op = 0, bits = 9, val = 163 },
                new code() { op = 0, bits = 8, val = 1 },
                new code() { op = 0, bits = 8, val = 129 },
                new code() { op = 0, bits = 8, val = 65 },
                new code() { op = 0, bits = 9, val = 227 },
                new code() { op = 16, bits = 7, val = 6 },
                new code() { op = 0, bits = 8, val = 89 },
                new code() { op = 0, bits = 8, val = 25 },
                new code() { op = 0, bits = 9, val = 147 },
                new code() { op = 19, bits = 7, val = 59 },
                new code() { op = 0, bits = 8, val = 121 },
                new code() { op = 0, bits = 8, val = 57 },
                new code() { op = 0, bits = 9, val = 211 },
                new code() { op = 17, bits = 7, val = 17 },
                new code() { op = 0, bits = 8, val = 105 },
                new code() { op = 0, bits = 8, val = 41 },
                new code() { op = 0, bits = 9, val = 179 },
                new code() { op = 0, bits = 8, val = 9 },
                new code() { op = 0, bits = 8, val = 137 },
                new code() { op = 0, bits = 8, val = 73 },
                new code() { op = 0, bits = 9, val = 243 },
                new code() { op = 16, bits = 7, val = 4 },
                new code() { op = 0, bits = 8, val = 85 },
                new code() { op = 0, bits = 8, val = 21 },
                new code() { op = 16, bits = 8, val = 258 },
                new code() { op = 19, bits = 7, val = 43 },
                new code() { op = 0, bits = 8, val = 117 },
                new code() { op = 0, bits = 8, val = 53 },
                new code() { op = 0, bits = 9, val = 203 },
                new code() { op = 17, bits = 7, val = 13 },
                new code() { op = 0, bits = 8, val = 101 },
                new code() { op = 0, bits = 8, val = 37 },
                new code() { op = 0, bits = 9, val = 171 },
                new code() { op = 0, bits = 8, val = 5 },
                new code() { op = 0, bits = 8, val = 133 },
                new code() { op = 0, bits = 8, val = 69 },
                new code() { op = 0, bits = 9, val = 235 },
                new code() { op = 16, bits = 7, val = 8 },
                new code() { op = 0, bits = 8, val = 93 },
                new code() { op = 0, bits = 8, val = 29 },
                new code() { op = 0, bits = 9, val = 155 },
                new code() { op = 20, bits = 7, val = 83 },
                new code() { op = 0, bits = 8, val = 125 },
                new code() { op = 0, bits = 8, val = 61 },
                new code() { op = 0, bits = 9, val = 219 },
                new code() { op = 18, bits = 7, val = 23 },
                new code() { op = 0, bits = 8, val = 109 },
                new code() { op = 0, bits = 8, val = 45 },
                new code() { op = 0, bits = 9, val = 187 },
                new code() { op = 0, bits = 8, val = 13 },
                new code() { op = 0, bits = 8, val = 141 },
                new code() { op = 0, bits = 8, val = 77 },
                new code() { op = 0, bits = 9, val = 251 },
                new code() { op = 16, bits = 7, val = 3 },
                new code() { op = 0, bits = 8, val = 83 },
                new code() { op = 0, bits = 8, val = 19 },
                new code() { op = 21, bits = 8, val = 195 },
                new code() { op = 19, bits = 7, val = 35 },
                new code() { op = 0, bits = 8, val = 115 },
                new code() { op = 0, bits = 8, val = 51 },
                new code() { op = 0, bits = 9, val = 199 },
                new code() { op = 17, bits = 7, val = 11 },
                new code() { op = 0, bits = 8, val = 99 },
                new code() { op = 0, bits = 8, val = 35 },
                new code() { op = 0, bits = 9, val = 167 },
                new code() { op = 0, bits = 8, val = 3 },
                new code() { op = 0, bits = 8, val = 131 },
                new code() { op = 0, bits = 8, val = 67 },
                new code() { op = 0, bits = 9, val = 231 },
                new code() { op = 16, bits = 7, val = 7 },
                new code() { op = 0, bits = 8, val = 91 },
                new code() { op = 0, bits = 8, val = 27 },
                new code() { op = 0, bits = 9, val = 151 },
                new code() { op = 20, bits = 7, val = 67 },
                new code() { op = 0, bits = 8, val = 123 },
                new code() { op = 0, bits = 8, val = 59 },
                new code() { op = 0, bits = 9, val = 215 },
                new code() { op = 18, bits = 7, val = 19 },
                new code() { op = 0, bits = 8, val = 107 },
                new code() { op = 0, bits = 8, val = 43 },
                new code() { op = 0, bits = 9, val = 183 },
                new code() { op = 0, bits = 8, val = 11 },
                new code() { op = 0, bits = 8, val = 139 },
                new code() { op = 0, bits = 8, val = 75 },
                new code() { op = 0, bits = 9, val = 247 },
                new code() { op = 16, bits = 7, val = 5 },
                new code() { op = 0, bits = 8, val = 87 },
                new code() { op = 0, bits = 8, val = 23 },
                new code() { op = 64, bits = 8, val = 0 },
                new code() { op = 19, bits = 7, val = 51 },
                new code() { op = 0, bits = 8, val = 119 },
                new code() { op = 0, bits = 8, val = 55 },
                new code() { op = 0, bits = 9, val = 207 },
                new code() { op = 17, bits = 7, val = 15 },
                new code() { op = 0, bits = 8, val = 103 },
                new code() { op = 0, bits = 8, val = 39 },
                new code() { op = 0, bits = 9, val = 175 },
                new code() { op = 0, bits = 8, val = 7 },
                new code() { op = 0, bits = 8, val = 135 },
                new code() { op = 0, bits = 8, val = 71 },
                new code() { op = 0, bits = 9, val = 239 },
                new code() { op = 16, bits = 7, val = 9 },
                new code() { op = 0, bits = 8, val = 95 },
                new code() { op = 0, bits = 8, val = 31 },
                new code() { op = 0, bits = 9, val = 159 },
                new code() { op = 20, bits = 7, val = 99 },
                new code() { op = 0, bits = 8, val = 127 },
                new code() { op = 0, bits = 8, val = 63 },
                new code() { op = 0, bits = 9, val = 223 },
                new code() { op = 18, bits = 7, val = 27 },
                new code() { op = 0, bits = 8, val = 111 },
                new code() { op = 0, bits = 8, val = 47 },
                new code() { op = 0, bits = 9, val = 191 },
                new code() { op = 0, bits = 8, val = 15 },
                new code() { op = 0, bits = 8, val = 143 },
                new code() { op = 0, bits = 8, val = 79 },
                new code() { op = 0, bits = 9, val = 255 }
        };

        public static code[] distfix = new code[]
        {
                new code() { op = 16, bits = 5, val = 1 },
                new code() { op = 23, bits = 5, val = 257 },
                new code() { op = 19, bits = 5, val = 17 },
                new code() { op = 27, bits = 5, val = 4097 },
                new code() { op = 17, bits = 5, val = 5 },
                new code() { op = 25, bits = 5, val = 1025 },
                new code() { op = 21, bits = 5, val = 65 },
                new code() { op = 29, bits = 5, val = 16385 },
                new code() { op = 16, bits = 5, val = 3 },
                new code() { op = 24, bits = 5, val = 513 },
                new code() { op = 20, bits = 5, val = 33 },
                new code() { op = 28, bits = 5, val = 8193 },
                new code() { op = 18, bits = 5, val = 9 },
                new code() { op = 26, bits = 5, val = 2049 },
                new code() { op = 22, bits = 5, val = 129 },
                new code() { op = 64, bits = 5, val = 0 },
                new code() { op = 16, bits = 5, val = 2 },
                new code() { op = 23, bits = 5, val = 385 },
                new code() { op = 19, bits = 5, val = 25 },
                new code() { op = 27, bits = 5, val = 6145 },
                new code() { op = 17, bits = 5, val = 7 },
                new code() { op = 25, bits = 5, val = 1537 },
                new code() { op = 21, bits = 5, val = 97 },
                new code() { op = 29, bits = 5, val = 24577 },
                new code() { op = 16, bits = 5, val = 4 },
                new code() { op = 24, bits = 5, val = 769 },
                new code() { op = 20, bits = 5, val = 49 },
                new code() { op = 28, bits = 5, val = 12289 },
                new code() { op = 18, bits = 5, val = 13 },
                new code() { op = 26, bits = 5, val = 3073 },
                new code() { op = 22, bits = 5, val = 193 },
                new code() { op = 64, bits = 5, val = 0 }
        };
        public static ushort[] inflate_order = new ushort[] { 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 };

        public static string deflateInit2__my_version = zlibVersion(); //Nanook changed to string
        public static ushort[] inflate_table_lbase = new ushort[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67, 83, 99, 115, 131, 163, 195, 227, 258, 0, 0 };
        public static ushort[] inflate_table_lext = new ushort[] { 16, 16, 16, 16, 16, 16, 16, 16, 17, 17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 19, 20, 20, 20, 20, 21, 21, 21, 21, 16, 199, 202 };
        public static ushort[] inflate_table_dbase = new ushort[] { 1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769, 1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577, 0, 0 };
        public static ushort[] inflate_table_dext = new ushort[] { 16, 16, 16, 16, 17, 17, 18, 18, 19, 19, 20, 20, 21, 21, 22, 22, 23, 23, 24, 24, 25, 25, 26, 26, 27, 27, 28, 28, 29, 29, 64, 64 };
        public static string zlibVersion()
        {
            return "1.2.12";
        }

        public static int deflate(z_stream_s strm, int flush)
        {
            int old_flush = 0;
            internal_state? s;
            if ((((deflateStateCheck(strm)) != 0) || ((flush) > (5))) || ((flush) < (0)))
            {
                return (int)(-2);
            }

            s = strm.state;
            if ((((strm.next_out) == (null)) || ((strm.avail_in != 0) && ((strm.next_in) == (null)))) || (((s!.status) == (666)) && (flush != 4)))
            {
                return -2; //Nanook (int)(strm.msg = z_errmsg[2 - (-2)], (-2));
            }

            if ((strm.avail_out) == (0))
                return -5; //Nanook (int)(strm.msg = z_errmsg[2 - (-5)], (-5));
            old_flush = (int)(s.last_flush);
            s.last_flush = (int)(flush);
            if (s.pending != 0)
            {
                flush_pending(strm);
                if ((strm.avail_out) == (0))
                {
                    s.last_flush = (int)(-1);
                    return (int)(0);
                }
            }
            else if ((((strm.avail_in) == (0)) && ((((flush) * 2) - ((flush) > (4) ? 9 : 0)) <= (((old_flush) * 2) - ((old_flush) > (4) ? 9 : 0)))) && (flush != 4))
            {
                return -5; //Nanook(int)(strm.msg = z_errmsg[2 - (-5)], (-5));
            }

            if (((s.status) == (666)) && (strm.avail_in != 0))
            {
                return -5; //Nanook(int)(strm.msg = z_errmsg[2 - (-5)], (-5));
            }

            if (((s.status) == (42)) && ((s.wrap) == (0)))
                s.status = (int)(113);
            if ((s.status) == (42))
            {
                uint header = (uint)((8 + ((s.w_bits - 8) << 4)) << 8);
                uint level_flags = 0;
                if (((s.strategy) >= (2)) || ((s.level) < (2)))
                    level_flags = (uint)(0);
                else if ((s.level) < (6))
                    level_flags = (uint)(1);
                else if ((s.level) == (6))
                    level_flags = (uint)(2);
                else
                    level_flags = (uint)(3);
                header |= (uint)(level_flags << 6);
                if (s.strstart != 0)
                    header |= (uint)(0x20);
                header += (uint)(31 - (header % 31));
                putShortMSB(s, (uint)(header));
                if (s.strstart != 0)
                {
                    putShortMSB(s, (uint)(strm.adler >> 16));
                    putShortMSB(s, (uint)(strm.adler & 0xffff));
                }

                strm.adler = (uint)(adler32((uint)(0L), null, (uint)(0)));
                s.status = (int)(113);
                flush_pending(strm);
                if (s.pending != 0)
                {
                    s.last_flush = (int)(-1);
                    return (int)(0);
                }
            }

            if ((s.status) == (57))
            {
                strm.adler = (uint)(crc32((uint)(0L), null, (uint)(0)));
                {
                    s.pending_buf[s.pending++] = ((byte)(31));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)(139));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)(8));
                }

                if ((s.gzhead) == (null))
                {
                    {
                        s.pending_buf[s.pending++] = ((byte)(0));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(0));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(0));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(0));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(0));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((s.level) == (9) ? 2 : (((s.strategy) >= (2)) || ((s.level) < (2)) ? 4 : 0)));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(10));
                    }

                    s.status = (int)(113);
                    flush_pending(strm);
                    if (s.pending != 0)
                    {
                        s.last_flush = (int)(-1);
                        return (int)(0);
                    }
                }
                else
                {
                    {
                        s.pending_buf[s.pending++] = ((byte)(((s.gzhead->text) != 0 ? 1 : 0) + ((s.gzhead->hcrc) != 0 ? 2 : 0) + ((s.gzhead->extra) == (null) ? 0 : 4) + ((s.gzhead->name) == (null) ? 0 : 8) + ((s.gzhead->comment) == (null) ? 0 : 16)));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(s.gzhead->time & 0xff));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((s.gzhead->time >> 8) & 0xff));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((s.gzhead->time >> 16) & 0xff));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((s.gzhead->time >> 24) & 0xff));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((s.level) == (9) ? 2 : (((s.strategy) >= (2)) || ((s.level) < (2)) ? 4 : 0)));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(s.gzhead->os & 0xff));
                    }

                    if (s.gzhead->extra != null)
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)(s.gzhead->extra_len & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.gzhead->extra_len >> 8) & 0xff));
                        }
                    }

                    if ((s.gzhead->hcrc) != 0)
                        strm.adler = (uint)(crc32((uint)(strm.adler), s.pending_buf, (uint)(s.pending)));
                    s.gzindex = (uint)(0);
                    s.status = (int)(69);
                }
            }

            if ((s.status) == (69))
            {
                if (s.gzhead->extra != null)
                {
                    uint beg = (uint)(s.pending);
                    uint left = (uint)((s.gzhead->extra_len & 0xffff) - s.gzindex);
                    while ((s.pending + left) > (s.pending_buf_size))
                    {
                        uint copy = (uint)(s.pending_buf_size - s.pending);
                        CRuntime.memcpy(s.pending_buf + s.pending, s.gzhead->extra + s.gzindex, (ulong)(copy));
                        s.pending = (uint)(s.pending_buf_size);
                        do
                        {
                            if (((s.gzhead->hcrc) != 0) && ((s.pending) > (beg)))
                                strm.adler = (uint)(crc32((uint)(strm.adler), s.pending_buf + (beg), (uint)(s.pending - (beg))));
                        }
                        while ((0) != 0);
                        s.gzindex += (uint)(copy);
                        flush_pending(strm);
                        if (s.pending != 0)
                        {
                            s.last_flush = (int)(-1);
                            return (int)(0);
                        }

                        beg = (uint)(0);
                        left -= (uint)(copy);
                    }

                    CRuntime.memcpy(s.pending_buf + s.pending, s.gzhead->extra + s.gzindex, (ulong)(left));
                    s.pending += (uint)(left);
                    do
                    {
                        if (((s.gzhead->hcrc) != 0) && ((s.pending) > (beg)))
                            strm.adler = (uint)(crc32((uint)(strm.adler), s.pending_buf + (beg), (uint)(s.pending - (beg))));
                    }
                    while ((0) != 0);
                    s.gzindex = (uint)(0);
                }

                s.status = (int)(73);
            }

            if ((s.status) == (73))
            {
                if (s.gzhead->name != null)
                {
                    uint beg = (uint)(s.pending);
                    int val = 0;
                    do
                    {
                        if ((s.pending) == (s.pending_buf_size))
                        {
                            do
                            {
                                if (((s.gzhead->hcrc) != 0) && ((s.pending) > (beg)))
                                    strm.adler = (uint)(crc32((uint)(strm.adler), s.pending_buf + (beg), (uint)(s.pending - (beg))));
                            }
                            while ((0) != 0);
                            flush_pending(strm);
                            if (s.pending != 0)
                            {
                                s.last_flush = (int)(-1);
                                return (int)(0);
                            }

                            beg = (uint)(0);
                        }

                        val = (int)(s.gzhead->name[s.gzindex++]);
                        {
                            s.pending_buf[s.pending++] = ((byte)(val));
                        }
                    }
                    while (val != 0);
                    do
                    {
                        if (((s.gzhead->hcrc) != 0) && ((s.pending) > (beg)))
                            strm.adler = (uint)(crc32((uint)(strm.adler), s.pending_buf + (beg), (uint)(s.pending - (beg))));
                    }
                    while ((0) != 0);
                    s.gzindex = (uint)(0);
                }

                s.status = (int)(91);
            }

            if ((s.status) == (91))
            {
                if (s.gzhead->comment != null)
                {
                    uint beg = (uint)(s.pending);
                    int val = 0;
                    do
                    {
                        if ((s.pending) == (s.pending_buf_size))
                        {
                            do
                            {
                                if (((s.gzhead->hcrc) != 0) && ((s.pending) > (beg)))
                                    strm.adler = (uint)(crc32((uint)(strm.adler), s.pending_buf + (beg), (uint)(s.pending - (beg))));
                            }
                            while ((0) != 0);
                            flush_pending(strm);
                            if (s.pending != 0)
                            {
                                s.last_flush = (int)(-1);
                                return (int)(0);
                            }

                            beg = (uint)(0);
                        }

                        val = (int)(s.gzhead->comment[s.gzindex++]);
                        {
                            s.pending_buf[s.pending++] = ((byte)(val));
                        }
                    }
                    while (val != 0);
                    do
                    {
                        if (((s.gzhead->hcrc) != 0) && ((s.pending) > (beg)))
                            strm.adler = (uint)(crc32((uint)(strm.adler), s.pending_buf + (beg), (uint)(s.pending - (beg))));
                    }
                    while ((0) != 0);
                }

                s.status = (int)(103);
            }

            if ((s.status) == (103))
            {
                if ((s.gzhead->hcrc) != 0)
                {
                    if ((s.pending + 2) > (s.pending_buf_size))
                    {
                        flush_pending(strm);
                        if (s.pending != 0)
                        {
                            s.last_flush = (int)(-1);
                            return (int)(0);
                        }
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)(strm.adler & 0xff));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((strm.adler >> 8) & 0xff));
                    }

                    strm.adler = (uint)(crc32((uint)(0L), null, (uint)(0)));
                }

                s.status = (int)(113);
                flush_pending(strm);
                if (s.pending != 0)
                {
                    s.last_flush = (int)(-1);
                    return (int)(0);
                }
            }

            if (((strm.avail_in != 0) || (s.lookahead != 0)) || ((flush != 0) && (s.status != 666)))
            {
                block_state bstate = new block_state();
                bstate = (s.level == 0 ? deflate_stored(s, flush) :
                      (s.strategy == 2 ? deflate_huff(s, flush) :
                      (s.strategy == 3 ? deflate_rle(s, flush) :
                                         configuration_table[s.level].func(s, flush))));
                if (((bstate) == (block_state.finish_started)) || ((bstate) == (block_state.finish_done)))
                {
                    s.status = (int)(666);
                }

                if (((bstate) == (block_state.need_more)) || ((bstate) == (block_state.finish_started)))
                {
                    if ((strm.avail_out) == (0))
                    {
                        s.last_flush = (int)(-1);
                    }

                    return (int)(0);
                }

                if ((bstate) == (block_state.block_done))
                {
                    if ((flush) == (1))
                    {
                        _tr_align(s);
                    }
                    else if (flush != 5)
                    {
                        _tr_stored_block(s, null, (uint)(0L), (int)(0));
                        if ((flush) == (3))
                        {
                            do
                            {
                                s.head[s.hash_size - 1] = (ushort)(0);
                                CRuntime.memset((byte*)(s.head), (int)(0), (ulong)((s.hash_size - 1) * sizeof(ushort)));
                            }
                            while ((0) != 0);
                            if ((s.lookahead) == (0))
                            {
                                s.strstart = (uint)(0);
                                s.block_start = (int)(0L);
                                s.insert = (uint)(0);
                            }
                        }
                    }

                    flush_pending(strm);
                    if ((strm.avail_out) == (0))
                    {
                        s.last_flush = (int)(-1);
                        return (int)(0);
                    }
                }
            }

            if (flush != 4)
                return (int)(0);
            if ((s.wrap) <= (0))
                return (int)(1);
            if ((s.wrap) == (2))
            {
                {
                    s.pending_buf[s.pending++] = ((byte)(strm.adler & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)((strm.adler >> 8) & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)((strm.adler >> 16) & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)((strm.adler >> 24) & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)(strm.total_in & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)((strm.total_in >> 8) & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)((strm.total_in >> 16) & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)((strm.total_in >> 24) & 0xff));
                }
            }
            else
            {
                putShortMSB(s, (uint)(strm.adler >> 16));
                putShortMSB(s, (uint)(strm.adler & 0xffff));
            }

            flush_pending(strm);
            if ((s.wrap) > (0))
                s.wrap = (int)(-s.wrap);
            return (int)(s.pending != 0 ? 0 : 1);
        }

        public static int deflateEnd(z_stream_s strm)
        {
            int status = 0;
            if ((deflateStateCheck(strm)) != 0)
                return (int)(-2);
            status = (int)(strm.state!.status);

            strm.state.pending_bufArray?.Free();
            strm.state.headArray?.Free();
            strm.state.prevArray?.Free();
            strm.state.windowArray?.Free();

            //*strm.zfree(strm.opaque, (void*)(strm.state)); //Nanook not malloced
            //strm.state = null;
            return (int)((status) == (113) ? (-3) : 0);
        }

        public static int inflate(z_stream_s strm, int flush)
        {
            inflate_state? state;
            byte* next;
            byte* put;
            uint have = 0; uint left = 0;
            uint hold = 0;
            uint bits = 0;
            uint _in_ = 0; uint _out_ = 0;
            uint copy = 0;
            byte* from;
            code here = new code();
            code last = new code();
            uint len = 0;
            int ret = 0;
            byte* hbuf = stackalloc byte[4];
            if ((((inflateStateCheck(strm)) != 0) || ((strm.next_out) == (null))) || (((strm.next_in) == (null)) && (strm.avail_in != 0)))
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if ((state!.mode) == (inflate_mode.TYPE))
                state.mode = (inflate_mode.TYPEDO);
            do
            {
                put = strm.next_out;
                left = (uint)(strm.avail_out);
                next = strm.next_in;
                have = (uint)(strm.avail_in);
                hold = (uint)(state.hold);
                bits = (uint)(state.bits);
            }
            while ((0) != 0);
            _in_ = (uint)(have);
            _out_ = (uint)(left);
            ret = (int)(0);
            for (; ; )
            {
                switch (state.mode)
                {
                    case inflate_mode.HEAD:
                        if ((state.wrap) == (0))
                        {
                            state.mode = (inflate_mode.TYPEDO);
                            break;
                        }

                        do
                        {
                            while ((bits) < ((uint)(16)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        if (((state.wrap & 2) != 0) && ((hold) == (0x8b1f)))
                        {
                            if ((state.wbits) == (0))
                                state.wbits = (uint)(15);
                            state.check = (uint)(crc32((uint)(0L), null, (uint)(0)));
                            do
                            {
                                hbuf[0] = ((byte)(hold));
                                hbuf[1] = ((byte)((hold) >> 8));
                                state.check = (uint)(crc32((uint)(state.check), hbuf, (uint)(2)));
                            }
                            while ((0) != 0);
                            do
                            {
                                hold = (uint)(0);
                                bits = (uint)(0);
                            }
                            while ((0) != 0);
                            state.mode = (inflate_mode.FLAGS);
                            break;
                        }

                        if (state.head != null)
                            state.head->done = (int)(-1);
                        if (((state.wrap & 1) == 0) || ((((hold & ((1U << (8)) - 1)) << 8) + (hold >> 8)) % 31) != 0)
                        {
                            strm.msg = "incorrect header check";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        if ((hold & ((1U << (4)) - 1)) != 8)
                        {
                            strm.msg = "unknown compression method";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        do
                        {
                            hold >>= (4);
                            bits -= ((uint)(4));
                        }
                        while ((0) != 0);
                        len = (uint)((hold & ((1U << (4)) - 1)) + 8);
                        if ((state.wbits) == (0))
                            state.wbits = (uint)(len);
                        if (((len) > (15)) || ((len) > (state.wbits)))
                        {
                            strm.msg = "invalid window size";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.dmax = (uint)(1U << (int)len);
                        state.flags = (int)(0);
                        strm.adler = (uint)(state.check = (uint)(adler32((uint)(0L), null, (uint)(0))));
                        state.mode = (inflate_mode)((hold & 0x200) != 0 ? inflate_mode.DICTID : inflate_mode.TYPE);
                        do
                        {
                            hold = (uint)(0);
                            bits = (uint)(0);
                        }
                        while ((0) != 0);
                        break;
                    case inflate_mode.FLAGS:
                        do
                        {
                            while ((bits) < ((uint)(16)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        state.flags = ((int)(hold));
                        if ((state.flags & 0xff) != 8)
                        {
                            strm.msg = "unknown compression method";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        if ((state.flags & 0xe000) != 0)
                        {
                            strm.msg = "unknown header flags set";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        if (state.head != null)
                            state.head->text = ((int)((hold >> 8) & 1));
                        if (((state.flags & 0x0200) != 0) && ((state.wrap & 4) != 0))
                            do
                            {
                                hbuf[0] = ((byte)(hold));
                                hbuf[1] = ((byte)((hold) >> 8));
                                state.check = (uint)(crc32((uint)(state.check), hbuf, (uint)(2)));
                            }
                            while ((0) != 0);
                        do
                        {
                            hold = (uint)(0);
                            bits = (uint)(0);
                        }
                        while ((0) != 0);
                        state.mode = (inflate_mode.TIME);
                        goto resumeTIME;
                    case inflate_mode.TIME:
                    resumeTIME:
                        do
                        {
                            while ((bits) < ((uint)(32)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        if (state.head != null)
                            state.head->time = (uint)(hold);
                        if (((state.flags & 0x0200) != 0) && ((state.wrap & 4) != 0))
                            do
                            {
                                hbuf[0] = ((byte)(hold));
                                hbuf[1] = ((byte)((hold) >> 8));
                                hbuf[2] = ((byte)((hold) >> 16));
                                hbuf[3] = ((byte)((hold) >> 24));
                                state.check = (uint)(crc32((uint)(state.check), hbuf, (uint)(4)));
                            }
                            while ((0) != 0);
                        do
                        {
                            hold = (uint)(0);
                            bits = (uint)(0);
                        }
                        while ((0) != 0);
                        state.mode = (inflate_mode.OS);
                        goto resumeOS;
                    case inflate_mode.OS:
                    resumeOS:
                        do
                        {
                            while ((bits) < ((uint)(16)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        if (state.head != null)
                        {
                            state.head->xflags = ((int)(hold & 0xff));
                            state.head->os = ((int)(hold >> 8));
                        }

                        if (((state.flags & 0x0200) != 0) && ((state.wrap & 4) != 0))
                            do
                            {
                                hbuf[0] = ((byte)(hold));
                                hbuf[1] = ((byte)((hold) >> 8));
                                state.check = (uint)(crc32((uint)(state.check), hbuf, (uint)(2)));
                            }
                            while ((0) != 0);
                        do
                        {
                            hold = (uint)(0);
                            bits = (uint)(0);
                        }
                        while ((0) != 0);
                        state.mode = (inflate_mode.EXLEN);
                        goto resumeEXLEN;
                    case inflate_mode.EXLEN:
                    resumeEXLEN:
                        if ((state.flags & 0x0400) != 0)
                        {
                            do
                            {
                                while ((bits) < ((uint)(16)))
                                {
                                    do
                                    {
                                        if ((have) == (0))
                                            goto inf_leave;
                                        have--;
                                        hold += (uint)((uint)(*next++) << (int)bits);
                                        bits += (uint)(8);
                                    }
                                    while ((0) != 0);
                                }
                            }
                            while ((0) != 0);
                            state.length = (uint)(hold);
                            if (state.head != null)
                                state.head->extra_len = (uint)(hold);
                            if (((state.flags & 0x0200) != 0) && ((state.wrap & 4) != 0))
                                do
                                {
                                    hbuf[0] = ((byte)(hold));
                                    hbuf[1] = ((byte)((hold) >> 8));
                                    state.check = (uint)(crc32((uint)(state.check), hbuf, (uint)(2)));
                                }
                                while ((0) != 0);
                            do
                            {
                                hold = (uint)(0);
                                bits = (uint)(0);
                            }
                            while ((0) != 0);
                        }
                        else if (state.head != null)
                            state.head->extra = null;
                        state.mode = (inflate_mode.EXTRA);
                        goto resumeEXTRA;
                    case inflate_mode.EXTRA:
                    resumeEXTRA:
                        if ((state.flags & 0x0400) != 0)
                        {
                            copy = (uint)(state.length);
                            if ((copy) > (have))
                                copy = (uint)(have);
                            if ((copy) != 0)
                            {
                                if ((state.head != null) && (state.head->extra != null))
                                {
                                    len = (uint)(state.head->extra_len - state.length);
                                    CRuntime.memcpy(state.head->extra + len, next, (ulong)((len + copy) > (state.head->extra_max) ? state.head->extra_max - len : copy));
                                }

                                if (((state.flags & 0x0200) != 0) && ((state.wrap & 4) != 0))
                                    state.check = (uint)(crc32((uint)(state.check), next, (uint)(copy)));
                                have -= (uint)(copy);
                                next += copy;
                                state.length -= (uint)(copy);
                            }

                            if ((state.length) != 0)
                                goto inf_leave;
                        }

                        state.length = (uint)(0);
                        state.mode = (inflate_mode.NAME);
                        goto resumeNAME;
                    case inflate_mode.NAME:
                    resumeNAME:
                        if ((state.flags & 0x0800) != 0)
                        {
                            if ((have) == (0))
                                goto inf_leave;
                            copy = (uint)(0);
                            do
                            {
                                len = ((uint)(next[copy++]));
                                if (((state.head != null) && (state.head->name != null)) && ((state.length) < (state.head->name_max)))
                                    state.head->name[state.length++] = ((byte)(len));
                            }
                            while (((len) != 0) && ((copy) < (have)));
                            if (((state.flags & 0x0200) != 0) && ((state.wrap & 4) != 0))
                                state.check = (uint)(crc32((uint)(state.check), next, (uint)(copy)));
                            have -= (uint)(copy);
                            next += copy;
                            if ((len) != 0)
                                goto inf_leave;
                        }
                        else if (state.head != null)
                            state.head->name = null;
                        state.length = (uint)(0);
                        state.mode = (inflate_mode.COMMENT);
                        goto resumeCOMMENT;
                    case inflate_mode.COMMENT:
                    resumeCOMMENT:
                        if ((state.flags & 0x1000) != 0)
                        {
                            if ((have) == (0))
                                goto inf_leave;
                            copy = (uint)(0);
                            do
                            {
                                len = ((uint)(next[copy++]));
                                if (((state.head != null) && (state.head->comment != null)) && ((state.length) < (state.head->comm_max)))
                                    state.head->comment[state.length++] = ((byte)(len));
                            }
                            while (((len) != 0) && ((copy) < (have)));
                            if (((state.flags & 0x0200) != 0) && ((state.wrap & 4) != 0))
                                state.check = (uint)(crc32((uint)(state.check), next, (uint)(copy)));
                            have -= (uint)(copy);
                            next += copy;
                            if ((len) != 0)
                                goto inf_leave;
                        }
                        else if (state.head != null)
                            state.head->comment = null;
                        state.mode = (inflate_mode.HCRC);
                        goto resumeHCRC;
                    case inflate_mode.HCRC:
                    resumeHCRC:
                        if ((state.flags & 0x0200) != 0)
                        {
                            do
                            {
                                while ((bits) < ((uint)(16)))
                                {
                                    do
                                    {
                                        if ((have) == (0))
                                            goto inf_leave;
                                        have--;
                                        hold += (uint)((uint)(*next++) << (int)bits);
                                        bits += (uint)(8);
                                    }
                                    while ((0) != 0);
                                }
                            }
                            while ((0) != 0);
                            if (((state.wrap & 4) != 0) && (hold != (state.check & 0xffff)))
                            {
                                strm.msg = "header crc mismatch";
                                state.mode = (inflate_mode.BAD);
                                break;
                            }

                            do
                            {
                                hold = (uint)(0);
                                bits = (uint)(0);
                            }
                            while ((0) != 0);
                        }

                        if (state.head != null)
                        {
                            state.head->hcrc = (int)((state.flags >> 9) & 1);
                            state.head->done = (int)(1);
                        }

                        strm.adler = (uint)(state.check = (uint)(crc32((uint)(0L), null, (uint)(0))));
                        state.mode = (inflate_mode.TYPE);
                        break;
                    case inflate_mode.DICTID:
                        do
                        {
                            while ((bits) < ((uint)(32)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        strm.adler = (uint)(state.check = (uint)((((hold) >> 24) & 0xff) + (((hold) >> 8) & 0xff00) + (((hold) & 0xff00) << 8) + (((hold) & 0xff) << 24)));
                        do
                        {
                            hold = (uint)(0);
                            bits = (uint)(0);
                        }
                        while ((0) != 0);
                        state.mode = (inflate_mode.DICT);
                        goto resumeDICT;
                    case inflate_mode.DICT:
                    resumeDICT:
                        if ((state.havedict) == (0))
                        {
                            do
                            {
                                strm.next_out = put;
                                strm.avail_out = (uint)(left);
                                strm.next_in = next;
                                strm.avail_in = (uint)(have);
                                state.hold = (uint)(hold);
                                state.bits = (uint)(bits);
                            }
                            while ((0) != 0);
                            return (int)(2);
                        }

                        strm.adler = (uint)(state.check = (uint)(adler32((uint)(0L), null, (uint)(0))));
                        state.mode = (inflate_mode.TYPE);
                        goto resumeTYPE;
                    case inflate_mode.TYPE:
                    resumeTYPE:
                        if (((flush) == (5)) || ((flush) == (6)))
                            goto inf_leave;
                        goto resumeTYPEDO;
                    case inflate_mode.TYPEDO:
                    resumeTYPEDO:
                        if ((state.last) != 0)
                        {
                            do
                            {
                                hold >>= (int)(bits & 7); //Nanook was hold >>= (bits & 7);
                                bits -= (uint)(bits & 7);
                            }
                            while ((0) != 0);
                            state.mode = (inflate_mode.CHECK);
                            break;
                        }

                        do
                        {
                            while ((bits) < ((uint)(3)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        state.last = (int)(hold & ((1U << (1)) - 1));
                        do
                        {
                            hold >>= (1);
                            bits -= ((uint)(1));
                        }
                        while ((0) != 0);
                        switch ((hold & ((1U << (2)) - 1)))
                        {
                            case 0:
                                ;
                                state.mode = (inflate_mode.STORED);
                                break;
                            case 1:
                                fixedtables(state);
                                state.mode = (inflate_mode.LEN_);
                                if ((flush) == (6))
                                {
                                    do
                                    {
                                        hold >>= (2);
                                        bits -= ((uint)(2));
                                    }
                                    while ((0) != 0);
                                    goto inf_leave;
                                }

                                break;
                            case 2:
                                ;
                                state.mode = (inflate_mode.TABLE);
                                break;
                            case 3:
                                strm.msg = "invalid block type";
                                state.mode = (inflate_mode.BAD);
                                break;
                        }

                        do
                        {
                            hold >>= (2);
                            bits -= ((uint)(2));
                        }
                        while ((0) != 0);
                        break;
                    case inflate_mode.STORED:
                        do
                        {
                            hold >>= (int)(bits & 7); //Nanook was hold >>= bits & 7;
                            bits -= (uint)(bits & 7);
                        }
                        while ((0) != 0);
                        do
                        {
                            while ((bits) < ((uint)(32)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        if ((hold & 0xffff) != ((hold >> 16) ^ 0xffff))
                        {
                            strm.msg = "invalid stored block lengths";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.length = (uint)(hold & 0xffff);
                        do
                        {
                            hold = (uint)(0);
                            bits = (uint)(0);
                        }
                        while ((0) != 0);
                        state.mode = (inflate_mode.COPY_);
                        if ((flush) == (6))
                            goto inf_leave;
                        goto resumeCOPY_;
                    case inflate_mode.COPY_:
                    resumeCOPY_:
                        state.mode = (inflate_mode.COPY);
                        goto resumeCOPY;
                    case inflate_mode.COPY:
                    resumeCOPY:
                        copy = (uint)(state.length);
                        if ((copy) != 0)
                        {
                            if ((copy) > (have))
                                copy = (uint)(have);
                            if ((copy) > (left))
                                copy = (uint)(left);
                            if ((copy) == (0))
                                goto inf_leave;
                            CRuntime.memcpy(put, next, (ulong)(copy));
                            have -= (uint)(copy);
                            next += copy;
                            left -= (uint)(copy);
                            put += copy;
                            state.length -= (uint)(copy);
                            break;
                        }

                        state.mode = (inflate_mode.TYPE);
                        break;
                    case inflate_mode.TABLE:
                        do
                        {
                            while ((bits) < ((uint)(14)))
                            {
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }
                        }
                        while ((0) != 0);
                        state.nlen = (uint)((hold & ((1U << (5)) - 1)) + 257);
                        do
                        {
                            hold >>= (5);
                            bits -= ((uint)(5));
                        }
                        while ((0) != 0);
                        state.ndist = (uint)((hold & ((1U << (5)) - 1)) + 1);
                        do
                        {
                            hold >>= (5);
                            bits -= ((uint)(5));
                        }
                        while ((0) != 0);
                        state.ncode = (uint)((hold & ((1U << (4)) - 1)) + 4);
                        do
                        {
                            hold >>= (4);
                            bits -= ((uint)(4));
                        }
                        while ((0) != 0);
                        if (((state.nlen) > (286)) || ((state.ndist) > (30)))
                        {
                            strm.msg = "too many length or distance symbols";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.have = (uint)(0);
                        state.mode = (inflate_mode.LENLENS);
                        goto resumeLENLENS;
                    case inflate_mode.LENLENS:
                    resumeLENLENS:
                        while ((state.have) < (state.ncode))
                        {
                            do
                            {
                                while ((bits) < ((uint)(3)))
                                {
                                    do
                                    {
                                        if ((have) == (0))
                                            goto inf_leave;
                                        have--;
                                        hold += (uint)((uint)(*next++) << (int)bits);
                                        bits += (uint)(8);
                                    }
                                    while ((0) != 0);
                                }
                            }
                            while ((0) != 0);
                            state.lens[inflate_order[state.have++]] = ((ushort)(hold & ((1U << (3)) - 1)));
                            do
                            {
                                hold >>= (3);
                                bits -= ((uint)(3));
                            }
                            while ((0) != 0);
                        }

                        while ((state.have) < (19))
                        {
                            state.lens[inflate_order[state.have++]] = (ushort)(0);
                        }

                        state.lencodes = state.codesArray.Data; //Nanook set lencode
                        state.distcodes = state.codesArray.Data; //Nanook set lencode
                        state.next = state.codes;
                        state.lencode = (state.next);
                        state.lenbits = (uint)(7);
                        ret = (int)(inflate_table(codetype.CODES, state.lens, (uint)(19), state.codesArray.Data, ref state.next, ref state.lenbits, state.workArray.Data));
                        if ((ret) != 0)
                        {
                            strm.msg = "invalid code lengths set";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.have = (uint)(0);
                        state.mode = (inflate_mode.CODELENS);
                        goto resumeCODELENS;
                    case inflate_mode.CODELENS:
                    resumeCODELENS:
                        while ((state.have) < (state.nlen + state.ndist))
                        {
                            for (; ; )
                            {
                                here = (code)(state.lencodes![state.lencode + (hold & ((1U << (int)(state.lenbits)) - 1))]); //Nanook added (int)
                                if (((uint)(here.bits)) <= (bits))
                                    break;
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }

                            if ((here.val) < (16))
                            {
                                do
                                {
                                    hold >>= (here.bits);
                                    bits -= ((uint)(here.bits));
                                }
                                while ((0) != 0);
                                state.lens[state.have++] = (ushort)(here.val);
                            }
                            else
                            {
                                if ((here.val) == (16))
                                {
                                    do
                                    {
                                        while ((bits) < ((uint)(here.bits + 2)))
                                        {
                                            do
                                            {
                                                if ((have) == (0))
                                                    goto inf_leave;
                                                have--;
                                                hold += (uint)((uint)(*next++) << (int)bits);
                                                bits += (uint)(8);
                                            }
                                            while ((0) != 0);
                                        }
                                    }
                                    while ((0) != 0);
                                    do
                                    {
                                        hold >>= (here.bits);
                                        bits -= ((uint)(here.bits));
                                    }
                                    while ((0) != 0);
                                    if ((state.have) == (0))
                                    {
                                        strm.msg = "invalid bit length repeat";
                                        state.mode = (inflate_mode.BAD);
                                        break;
                                    }

                                    len = (uint)(state.lens[state.have - 1]);
                                    copy = (uint)(3 + (hold & ((1U << (2)) - 1)));
                                    do
                                    {
                                        hold >>= (2);
                                        bits -= ((uint)(2));
                                    }
                                    while ((0) != 0);
                                }
                                else if ((here.val) == (17))
                                {
                                    do
                                    {
                                        while ((bits) < ((uint)(here.bits + 3)))
                                        {
                                            do
                                            {
                                                if ((have) == (0))
                                                    goto inf_leave;
                                                have--;
                                                hold += (uint)((uint)(*next++) << (int)bits);
                                                bits += (uint)(8);
                                            }
                                            while ((0) != 0);
                                        }
                                    }
                                    while ((0) != 0);
                                    do
                                    {
                                        hold >>= (here.bits);
                                        bits -= ((uint)(here.bits));
                                    }
                                    while ((0) != 0);
                                    len = (uint)(0);
                                    copy = (uint)(3 + (hold & ((1U << (3)) - 1)));
                                    do
                                    {
                                        hold >>= (3);
                                        bits -= ((uint)(3));
                                    }
                                    while ((0) != 0);
                                }
                                else
                                {
                                    do
                                    {
                                        while ((bits) < ((uint)(here.bits + 7)))
                                        {
                                            do
                                            {
                                                if ((have) == (0))
                                                    goto inf_leave;
                                                have--;
                                                hold += (uint)((uint)(*next++) << (int)bits);
                                                bits += (uint)(8);
                                            }
                                            while ((0) != 0);
                                        }
                                    }
                                    while ((0) != 0);
                                    do
                                    {
                                        hold >>= (here.bits);
                                        bits -= ((uint)(here.bits));
                                    }
                                    while ((0) != 0);
                                    len = (uint)(0);
                                    copy = (uint)(11 + (hold & ((1U << (7)) - 1)));
                                    do
                                    {
                                        hold >>= (7);
                                        bits -= ((uint)(7));
                                    }
                                    while ((0) != 0);
                                }

                                if ((state.have + copy) > (state.nlen + state.ndist))
                                {
                                    strm.msg = "invalid bit length repeat";
                                    state.mode = (inflate_mode.BAD);
                                    break;
                                }

                                while ((copy--) != 0)
                                {
                                    state.lens[state.have++] = ((ushort)(len));
                                }
                            }
                        }

                        if ((state.mode) == (inflate_mode.BAD))
                            break;
                        if ((state.lens[256]) == (0))
                        {
                            strm.msg = "invalid code -- missing end-of-block";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.lencodes = state.codesArray.Data; //Nanook set lencode
                        state.distcodes = state.codesArray.Data; //Nanook set lencode
                        state.next = state.codes;
                        state.lencode = (state.next);
                        state.lenbits = (uint)(9);
                        ret = (int)(inflate_table(codetype.LENS, state.lens, (uint)(state.nlen), state.lencodes, ref state.next, ref state.lenbits, state.workArray.Data));
                        if ((ret) != 0)
                        {
                            strm.msg = "invalid literal/lengths set";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.distcode = (state.next);
                        state.distbits = (uint)(6);
                        ret = (int)(inflate_table(codetype.DISTS, state.lens + state.nlen, (uint)(state.ndist), state.distcodes, ref state.next, ref state.distbits, state.workArray.Data));
                        if ((ret) != 0)
                        {
                            strm.msg = "invalid distances set";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.mode = (inflate_mode.LEN_);
                        if ((flush) == (6))
                            goto inf_leave;
                        goto resumeLEN_;
                    case inflate_mode.LEN_:
                    resumeLEN_:
                        state.mode = (inflate_mode.LEN);
                        goto resumeLEN;
                    case inflate_mode.LEN:
                    resumeLEN:
                        if (((have) >= (6)) && ((left) >= (258)))
                        {
                            do
                            {
                                strm.next_out = put;
                                strm.avail_out = (uint)(left);
                                strm.next_in = next;
                                strm.avail_in = (uint)(have);
                                state.hold = (uint)(hold);
                                state.bits = (uint)(bits);
                            }
                            while ((0) != 0);
                            inflate_fast(strm, (uint)(_out_));
                            do
                            {
                                put = strm.next_out;
                                left = (uint)(strm.avail_out);
                                next = strm.next_in;
                                have = (uint)(strm.avail_in);
                                hold = (uint)(state.hold);
                                bits = (uint)(state.bits);
                            }
                            while ((0) != 0);
                            if ((state.mode) == (inflate_mode.TYPE))
                                state.back = (int)(-1);
                            break;
                        }

                        state.back = (int)(0);
                        for (; ; )
                        {
                            here = (code)state.lencodes![(int)(state.lencode + (hold & ((1U << (int)(state.lenbits)) - 1)))]; //Nanook added (int)
                            if (((uint)(here.bits)) <= (bits))
                                break;
                            do
                            {
                                if ((have) == (0))
                                    goto inf_leave;
                                have--;
                                hold += (uint)((uint)(*next++) << (int)bits);
                                bits += (uint)(8);
                            }
                            while ((0) != 0);
                        }

                        if (((here.op) != 0) && ((here.op & 0xf0) == (0)))
                        {
                            last = (code)(here);
                            for (; ; )
                            {
                                here = (code)(state.lencodes[state.lencode + last.val + ((hold & ((1U << (last.bits + last.op)) - 1)) >> last.bits)]);
                                if (((uint)(last.bits + here.bits)) <= (bits))
                                    break;
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }

                            do
                            {
                                hold >>= (last.bits);
                                bits -= ((uint)(last.bits));
                            }
                            while ((0) != 0);
                            state.back += (int)(last.bits);
                        }

                        do
                        {
                            hold >>= (here.bits);
                            bits -= ((uint)(here.bits));
                        }
                        while ((0) != 0);
                        state.back += (int)(here.bits);
                        state.length = ((uint)(here.val));
                        if (((int)(here.op)) == (0))
                        {
                            state.mode = (inflate_mode.LIT);
                            break;
                        }

                        if ((here.op & 32) != 0)
                        {
                            state.back = (int)(-1);
                            state.mode = (inflate_mode.TYPE);
                            break;
                        }

                        if ((here.op & 64) != 0)
                        {
                            strm.msg = "invalid literal/length code";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.extra = (uint)((uint)(here.op) & 15);
                        state.mode = (inflate_mode.LENEXT);
                        goto resumeLENEXT;
                    case inflate_mode.LENEXT:
                    resumeLENEXT:
                        if ((state.extra) != 0)
                        {
                            do
                            {
                                while ((bits) < (state.extra))
                                {
                                    do
                                    {
                                        if ((have) == (0))
                                            goto inf_leave;
                                        have--;
                                        hold += (uint)((uint)(*next++) << (int)bits);
                                        bits += (uint)(8);
                                    }
                                    while ((0) != 0);
                                }
                            }
                            while ((0) != 0);
                            state.length += (uint)(hold & ((1U << (int)(state.extra)) - 1)); //Nanook added (int)
                            do
                            {
                                hold >>= (int)(state.extra); //Nanook added (int)
                                bits -= (uint)(state.extra);
                            }
                            while ((0) != 0);
                            state.back += (int)(state.extra);
                        }

                        state.was = (uint)(state.length);
                        state.mode = (inflate_mode.DIST);
                        goto resumeDIST;
                    case inflate_mode.DIST:
                    resumeDIST:
                        for (; ; )
                        {
                            here = (code)state.distcodes![(state.distcode + (hold & ((1U << (int)(state.distbits)) - 1)))]; //Nanook added (int)
                            if (((uint)(here.bits)) <= (bits))
                                break;
                            do
                            {
                                if ((have) == (0))
                                    goto inf_leave;
                                have--;
                                hold += (uint)((uint)(*next++) << (int)bits);
                                bits += (uint)(8);
                            }
                            while ((0) != 0);
                        }

                        if ((here.op & 0xf0) == (0))
                        {
                            last = (code)(here);
                            for (; ; )
                            {
                                here = state.distcodes[(int)(state.distcode + last.val + ((hold & ((1U << (last.bits + last.op)) - 1)) >> last.bits))];
                                if (((uint)(last.bits + here.bits)) <= (bits))
                                    break;
                                do
                                {
                                    if ((have) == (0))
                                        goto inf_leave;
                                    have--;
                                    hold += (uint)((uint)(*next++) << (int)bits);
                                    bits += (uint)(8);
                                }
                                while ((0) != 0);
                            }

                            do
                            {
                                hold >>= (last.bits);
                                bits -= ((uint)(last.bits));
                            }
                            while ((0) != 0);
                            state.back += (int)(last.bits);
                        }

                        do
                        {
                            hold >>= (here.bits);
                            bits -= ((uint)(here.bits));
                        }
                        while ((0) != 0);
                        state.back += (int)(here.bits);
                        if ((here.op & 64) != 0)
                        {
                            strm.msg = "invalid distance code";
                            state.mode = (inflate_mode.BAD);
                            break;
                        }

                        state.offset = ((uint)(here.val));
                        state.extra = (uint)((uint)(here.op) & 15);
                        state.mode = (inflate_mode.DISTEXT);
                        goto resumeDISTEXT;
                    case inflate_mode.DISTEXT:
                    resumeDISTEXT:
                        if ((state.extra) != 0)
                        {
                            do
                            {
                                while ((bits) < (state.extra))
                                {
                                    do
                                    {
                                        if ((have) == (0))
                                            goto inf_leave;
                                        have--;
                                        hold += (uint)((uint)(*next++) << (int)bits);
                                        bits += (uint)(8);
                                    }
                                    while ((0) != 0);
                                }
                            }
                            while ((0) != 0);
                            state.offset += (uint)(hold & ((1U << (int)(state.extra)) - 1)); //Nanook added (int)
                            do
                            {
                                hold >>= (int)(state.extra); //Nanook added (int)
                                bits -= (uint)(state.extra);
                            }
                            while ((0) != 0);
                            state.back += (int)(state.extra);
                        }

                        state.mode = (inflate_mode.MATCH);
                        goto resumeMATCH;
                    case inflate_mode.MATCH:
                    resumeMATCH:
                        if ((left) == (0))
                            goto inf_leave;
                        copy = (uint)(_out_ - left);
                        if ((state.offset) > (copy))
                        {
                            copy = (uint)(state.offset - copy);
                            if ((copy) > (state.whave))
                            {
                                if ((state.sane) != 0)
                                {
                                    strm.msg = "invalid distance too far back";
                                    state.mode = (inflate_mode.BAD);
                                    break;
                                }
                            }

                            if ((copy) > (state.wnext))
                            {
                                copy -= (uint)(state.wnext);
                                from = state.window + (state.wsize - copy);
                            }
                            else
                                from = state.window + (state.wnext - copy);
                            if ((copy) > (state.length))
                                copy = (uint)(state.length);
                        }
                        else
                        {
                            from = put - state.offset;
                            copy = (uint)(state.length);
                        }

                        if ((copy) > (left))
                            copy = (uint)(left);
                        left -= (uint)(copy);
                        state.length -= (uint)(copy);
                        do
                        {
                            *put++ = (byte)(*from++);
                        }
                        while ((--copy) != 0);
                        if ((state.length) == (0))
                            state.mode = (inflate_mode.LEN);
                        break;
                    case inflate_mode.LIT:
                        if ((left) == (0))
                            goto inf_leave;
                        *put++ = ((byte)(state.length));
                        left--;
                        state.mode = (inflate_mode.LEN);
                        break;
                    case inflate_mode.CHECK:
                        if ((state.wrap) != 0)
                        {
                            do
                            {
                                while ((bits) < ((uint)(32)))
                                {
                                    do
                                    {
                                        if ((have) == (0))
                                            goto inf_leave;
                                        have--;
                                        hold += (uint)((uint)(*next++) << (int)bits);
                                        bits += (uint)(8);
                                    }
                                    while ((0) != 0);
                                }
                            }
                            while ((0) != 0);
                            _out_ -= (uint)(left);
                            strm.total_out += (uint)(_out_);
                            state.total += (uint)(_out_);
                            if (((state.wrap & 4) != 0) && ((_out_) != 0))
                                strm.adler = (uint)(state.check = (uint)((state.flags) != 0 ? crc32((uint)(state.check), put - _out_, (uint)(_out_)) : adler32((uint)(state.check), put - _out_, (uint)(_out_))));
                            _out_ = (uint)(left);
                            if (((state.wrap & 4) != 0) && (((state.flags) != 0 ? hold : ((((hold) >> 24) & 0xff) + (((hold) >> 8) & 0xff00) + (((hold) & 0xff00) << 8) + (((hold) & 0xff) << 24))) != state.check))
                            {
                                strm.msg = "incorrect data check";
                                state.mode = (inflate_mode.BAD);
                                break;
                            }

                            do
                            {
                                hold = (uint)(0);
                                bits = (uint)(0);
                            }
                            while ((0) != 0);
                        }

                        state.mode = (inflate_mode.LENGTH);
                        goto resumeLENGTH;
                    case inflate_mode.LENGTH:
                    resumeLENGTH:
                        if (((state.wrap) != 0) && ((state.flags) != 0))
                        {
                            do
                            {
                                while ((bits) < ((uint)(32)))
                                {
                                    do
                                    {
                                        if ((have) == (0))
                                            goto inf_leave;
                                        have--;
                                        hold += (uint)((uint)(*next++) << (int)bits);
                                        bits += (uint)(8);
                                    }
                                    while ((0) != 0);
                                }
                            }
                            while ((0) != 0);
                            if (((state.wrap & 4) != 0) && (hold != (state.total & 0xffffffff)))
                            {
                                strm.msg = "incorrect length check";
                                state.mode = (inflate_mode.BAD);
                                break;
                            }

                            do
                            {
                                hold = (uint)(0);
                                bits = (uint)(0);
                            }
                            while ((0) != 0);
                        }

                        state.mode = (inflate_mode.DONE);
                        goto resumeDONE;
                    case inflate_mode.DONE:
                    resumeDONE:
                        ret = (int)(1);
                        goto inf_leave;
                    case inflate_mode.BAD:
                        ret = (int)(-3);
                        goto inf_leave;
                    case inflate_mode.MEM:
                        return (int)(-4);
                    case inflate_mode.SYNC:
                    default:
                        return (int)(-2);
                }
            }

        inf_leave:
            ; do { strm.next_out = put; strm.avail_out = (uint)(left); strm.next_in = next; strm.avail_in = (uint)(have); state.hold = (uint)(hold); state.bits = (uint)(bits); } while ((0) != 0);
            if (((state.wsize) != 0) || (((_out_ != strm.avail_out) && ((state.mode) < (inflate_mode.BAD))) && (((state.mode) < (inflate_mode.CHECK)) || (flush != 4))))
                if ((updatewindow(strm, strm.next_out, (uint)(_out_ - strm.avail_out))) != 0)
                {
                    state.mode = (inflate_mode.MEM);
                    return (int)(-4);
                }

            _in_ -= (uint)(strm.avail_in);
            _out_ -= (uint)(strm.avail_out);
            strm.total_in += (uint)(_in_);
            strm.total_out += (uint)(_out_);
            state.total += (uint)(_out_);
            if (((state.wrap & 4) != 0) && ((_out_) != 0))
                strm.adler = (uint)(state.check = (uint)((state.flags) != 0 ? crc32((uint)(state.check), strm.next_out - _out_, (uint)(_out_)) : adler32((uint)(state.check), strm.next_out - _out_, (uint)(_out_))));
            strm.data_type = (int)((int)(state.bits) + ((state.last) != 0 ? 64 : 0) + ((state.mode) == (inflate_mode.TYPE) ? 128 : 0) + (((state.mode) == (inflate_mode.LEN_)) || ((state.mode) == (inflate_mode.COPY_)) ? 256 : 0));
            if (((((_in_) == (0)) && ((_out_) == (0))) || ((flush) == (4))) && ((ret) == (0)))
                ret = (int)(-5);
            return (int)(ret);
        }

        public static int inflateEnd(z_stream_s strm)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            //if (state.window != null)
            //{
            //    *((strm).zfree)((strm).opaque,(void*)(state.window));
            //}
            //*((strm).zfree)((strm).opaque, (void*)(strm.state));
            strm.i_state = null;
            return (int)(0);
        }

        public static int deflateSetDictionary(z_stream_s strm, byte* dictionary, uint dictLength)
        {
            internal_state? s;
            uint str = 0; uint  n  =  0 ;
            int wrap = 0;
            uint avail = 0;
            byte* next;
            if (((deflateStateCheck(strm)) != 0) || ((dictionary) == (null)))
                return (int)(-2);
            s = strm.state;
            wrap = (int)(s!.wrap);
            if ((((wrap) == (2)) || (((wrap) == (1)) && (s.status != 42))) || ((s.lookahead) != 0))
                return (int)(-2);
            if ((wrap) == (1))
                strm.adler = (uint)(adler32((uint)(strm.adler), dictionary, (uint)(dictLength)));
            s.wrap = (int)(0);
            if ((dictLength) >= (s.w_size))
            {
                if ((wrap) == (0))
                {
                    do
                    {
                        s.head[s.hash_size - 1] = (ushort)(0);
                        CRuntime.memset((byte*)(s.head), (int)(0), (ulong)((s.hash_size - 1) * sizeof(ushort)));
                    }
                    while ((0) != 0);
                    s.strstart = (uint)(0);
                    s.block_start = (int)(0L);
                    s.insert = (uint)(0);
                }

                dictionary += dictLength - s.w_size;
                dictLength = (uint)(s.w_size);
            }

            avail = (uint)(strm.avail_in);
            next = strm.next_in;
            strm.avail_in = (uint)(dictLength);
            strm.next_in = dictionary;
            fill_window(s);
            while ((s.lookahead) >= (3))
            {
                str = (uint)(s.strstart);
                n = (uint)(s.lookahead - (3 - 1));
                do
                {
                    s.ins_h = (uint)(((s.ins_h << (int)s.hash_shift) ^ s.window[str + 3 - 1]) & s.hash_mask); //Nanook added (int)
                    s.prev[str & s.w_mask] = (ushort)(s.head[s.ins_h]);
                    s.head[s.ins_h] = ((ushort)(str));
                    str++;
                }
                while ((--n) != 0);
                s.strstart = (uint)(str);
                s.lookahead = (uint)(3 - 1);
                fill_window(s);
            }

            s.strstart += (uint)(s.lookahead);
            s.block_start = ((int)(s.strstart));
            s.insert = (uint)(s.lookahead);
            s.lookahead = (uint)(0);
            s.match_length = (uint)(s.prev_length = (uint)(3 - 1));
            s.match_available = (int)(0);
            strm.next_in = next;
            strm.avail_in = (uint)(avail);
            s.wrap = (int)(wrap);
            return (int)(0);
        }

        public static int deflateGetDictionary(z_stream_s strm, byte* dictionary, uint* dictLength)
        {
            internal_state? s;
            uint len = 0;
            if ((deflateStateCheck(strm)) != 0)
                return (int)(-2);
            s = strm.state;
            len = (uint)(s!.strstart + s.lookahead);
            if ((len) > (s.w_size))
                len = (uint)(s.w_size);
            if ((dictionary != null) && ((len) != 0))
                CRuntime.memcpy(dictionary, s.window + s.strstart + s.lookahead - len, (ulong)(len));
            if (dictLength != null)
                *dictLength = (uint)(len);
            return (int)(0);
        }

//#warning implement deflatecopy() as a clone

        //public static int deflateCopy(z_stream_s dest, z_stream_s source)
        //{
        //    internal_state ds;
        //    internal_state ss;
        //    if (((deflateStateCheck(source)) != 0) || ((dest) == (null)))
        //    {
        //        return (int)(-2);
        //    }

        //    ss = source.state;
        //    CRuntime.memcpy((void*)(dest), (void*)(source), (ulong)(sizeof(z_stream_s)));
        //    ds = (internal_state)(*((dest).zalloc)((dest).opaque, (uint)(1), (uint)(sizeof(internal_state))));
        //    if ((ds) == (null))
        //        return (int)(-4);
        //    dest.state = ds;
        //    CRuntime.memcpy((void*)(ds), (void*)(ss), (ulong)(sizeof(internal_state)));
        //    ds.strm = dest;
        //    ds.window = (byte*)(*((dest).zalloc)((dest).opaque, (uint)(ds.w_size), (uint)(2 * sizeof(byte))));
        //    ds.prev = (ushort*)(*((dest).zalloc)((dest).opaque, (uint)(ds.w_size), (uint)(sizeof(ushort))));
        //    ds.head = (ushort*)(*((dest).zalloc)((dest).opaque, (uint)(ds.hash_size), (uint)(sizeof(ushort))));
        //    ds.pending_buf = (byte*)(*((dest).zalloc)((dest).opaque, (uint)(ds.lit_bufsize), (uint)(4)));
        //    if (((((ds.window) == (null)) || ((ds.prev) == (null))) || ((ds.head) == (null))) || ((ds.pending_buf) == (null)))
        //    {
        //        deflateEnd(dest);
        //        return (int)(-4);
        //    }

        //    CRuntime.memcpy(ds.window, ss.window, (ulong)(ds.w_size * 2 * sizeof(byte)));
        //    CRuntime.memcpy((void*)(ds.prev), (void*)(ss.prev), (ulong)(ds.w_size * sizeof(ushort)));
        //    CRuntime.memcpy((void*)(ds.head), (void*)(ss.head), (ulong)(ds.hash_size * sizeof(ushort)));
        //    CRuntime.memcpy(ds.pending_buf, ss.pending_buf, (ulong)(ds.pending_buf_size));
        //    ds.pending_out = ds.pending_buf + (ss.pending_out - ss.pending_buf);
        //    ds.sym_buf = ds.pending_buf + ds.lit_bufsize;
        //    ds.l_desc.dyn_tree = ds.dyn_ltree;
        //    ds.d_desc.dyn_tree = ds.dyn_dtree;
        //    ds.bl_desc.dyn_tree = ds.bl_tree;
        //    return (int)(0);
        //}

        public static int deflateReset(z_stream_s strm)
        {
            int ret = 0;
            ret = (int)(deflateResetKeep(strm));
            if ((ret) == (0))
                lm_init(strm.state!);
            return (int)(ret);
        }

        public static int deflateParams(z_stream_s strm, int level, int strategy)
        {
            internal_state? s;
            delegate2 func;
            if ((deflateStateCheck(strm)) != 0)
                return (int)(-2);
            s = strm.state;
            if ((level) == (-1))
                level = (int)(6);
            if (((((level) < (0)) || ((level) > (9))) || ((strategy) < (0))) || ((strategy) > (4)))
            {
                return (int)(-2);
            }

            func = configuration_table[s!.level].func;
            if (((strategy != s.strategy) || (func != configuration_table[level].func)) && (s.last_flush != -2))
            {
                int err = (int)(deflate(strm, (int)(5)));
                if ((err) == (-2))
                    return (int)(err);
                if (((strm.avail_in) != 0) || ((s.strstart - s.block_start) + s.lookahead) != 0) //Nanook added != 0
                    return (int)(-5);
            }

            if (s.level != level)
            {
                if (((s.level) == (0)) && (s.matches != 0))
                {
                    if ((s.matches) == (1))
                        slide_hash(s);
                    else
                        do
                        {
                            s.head[s.hash_size - 1] = (ushort)(0);
                            CRuntime.memset((byte*)(s.head), (int)(0), (ulong)((s.hash_size - 1) * sizeof(ushort)));
                        }
                        while ((0) != 0);
                    s.matches = (uint)(0);
                }

                s.level = (int)(level);
                s.max_lazy_match = (uint)(configuration_table[level].max_lazy);
                s.good_match = (uint)(configuration_table[level].good_length);
                s.nice_match = (int)(configuration_table[level].nice_length);
                s.max_chain_length = (uint)(configuration_table[level].max_chain);
            }

            s.strategy = (int)(strategy);
            return (int)(0);
        }

        public static int deflateTune(z_stream_s strm, int good_length, int max_lazy, int nice_length, int max_chain)
        {
            internal_state? s;
            if ((deflateStateCheck(strm)) != 0)
                return (int)(-2);
            s = strm.state;
            s!.good_match = ((uint)(good_length));
            s.max_lazy_match = ((uint)(max_lazy));
            s.nice_match = (int)(nice_length);
            s.max_chain_length = ((uint)(max_chain));
            return (int)(0);
        }

        public static uint deflateBound(z_stream_s strm, uint sourceLen)
        {
            internal_state? s;
            uint complen = 0; uint  wraplen  =  0 ;
            complen = (uint)(sourceLen + ((sourceLen + 7) >> 3) + ((sourceLen + 63) >> 6) + 5);
            if ((deflateStateCheck(strm)) != 0)
                return (uint)(complen + 6);
            s = strm.state;
            switch (s!.wrap)
            {
                case 0:
                    wraplen = (uint)(0);
                    break;
                case 1:
                    wraplen = (uint)(6 + ((s.strstart) != 0 ? 4 : 0));
                    break;
                case 2:
                    wraplen = (uint)(18);
                    if (s.gzhead != null)
                    {
                        byte* str;
                        if (s.gzhead->extra != null)
                            wraplen += (uint)(2 + s.gzhead->extra_len);
                        str = s.gzhead->name;
                        if (str != null)
                            do
                            {
                                wraplen++;
                            }
                            while ((*str++) != 0);
                        str = s.gzhead->comment;
                        if (str != null)
                            do
                            {
                                wraplen++;
                            }
                            while ((*str++) != 0);
                        if ((s.gzhead->hcrc) != 0)
                            wraplen += (uint)(2);
                    }

                    break;
                default:
                    wraplen = (uint)(6);
                    break; //Nanook
            }

            if ((s.w_bits != 15) || (s.hash_bits != 8 + 7))
                return (uint)(complen + wraplen);
            return (uint)(sourceLen + (sourceLen >> 12) + (sourceLen >> 14) + (sourceLen >> 25) + 13 - 6 + wraplen);
        }

        public static int deflatePending(z_stream_s strm, uint* pending, int* bits)
        {
            if ((deflateStateCheck(strm)) != 0)
                return (int)(-2);
            if (pending != null)
                *pending = (uint)(strm.state!.pending);
            if (bits != null)
                *bits = (int)(strm.state!.bi_valid);
            return (int)(0);
        }

        public static int deflatePrime(z_stream_s strm, int bits, int value)
        {
            internal_state? s;
            int put = 0;
            if ((deflateStateCheck(strm)) != 0)
                return (int)(-2);
            s = strm.state;
            if ((((bits) < (0)) || ((bits) > (16))) || ((s!.sym_buf) < (s.pending_out + ((16 + 7) >> 3))))
                return (int)(-5);
            do
            {
                put = (int)(16 - s.bi_valid);
                if ((put) > (bits))
                    put = (int)(bits);
                s.bi_buf |= ((ushort)((value & ((1 << put) - 1)) << s.bi_valid));
                s.bi_valid += (int)(put);
                _tr_flush_bits(s);
                value >>= put;
                bits -= (int)(put);
            }
            while ((bits) != 0);
            return (int)(0);
        }

        public static int deflateSetHeader(z_stream_s strm, gz_header_s* head)
        {
            if (((deflateStateCheck(strm)) != 0) || (strm.state!.wrap != 2))
                return (int)(-2);
            strm.state.gzhead = head;
            return (int)(0);
        }

        public static int inflateSetDictionary(z_stream_s strm, byte* dictionary, uint dictLength)
        {
            inflate_state? state;
            uint dictid = 0;
            int ret = 0;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if ((state.wrap != 0) && (state.mode != inflate_mode.DICT))
                return (int)(-2);
            if ((state.mode) == (inflate_mode.DICT))
            {
                dictid = (uint)(adler32((uint)(0L), null, (uint)(0)));
                dictid = (uint)(adler32((uint)(dictid), dictionary, (uint)(dictLength)));
                if (dictid != state.check)
                    return (int)(-3);
            }

            ret = (int)(updatewindow(strm, dictionary + dictLength, (uint)(dictLength)));
            if ((ret) != 0)
            {
                state.mode = (inflate_mode.MEM);
                return (int)(-4);
            }

            state.havedict = (int)(1);
            return (int)(0);
        }

        public static int inflateGetDictionary(z_stream_s strm, byte* dictionary, uint* dictLength)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if (((state.whave) != 0) && (dictionary != null))
            {
                CRuntime.memcpy(dictionary, state.window + state.wnext, (ulong)(state.whave - state.wnext));
                CRuntime.memcpy(dictionary + state.whave - state.wnext, state.window, (ulong)(state.wnext));
            }

            if (dictLength != null)
                *dictLength = (uint)(state.whave);
            return (int)(0);
        }

        public static int inflateSync(z_stream_s strm)
        {
            uint len = 0;
            int flags = 0;
            uint _in_ = 0; uint _out_ = 0;
            byte* buf = stackalloc byte[4];
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if (((strm.avail_in) == (0)) && ((state.bits) < (8)))
                return (int)(-5);
            if (state.mode != inflate_mode.SYNC)
            {
                state.mode = (inflate_mode.SYNC);
                state.hold <<= (int)(state.bits & 7); //Nanook added (int)
                state.bits -= (uint)(state.bits & 7);
                len = (uint)(0);
                while ((state.bits) >= (8))
                {
                    buf[len++] = ((byte)(state.hold));
                    state.hold >>= 8;
                    state.bits -= (uint)(8);
                }

                state.have = (uint)(0);
                fixed (uint* haveFx = &(state.have))
                    syncsearch(haveFx, buf, (uint)(len));
            }

            fixed (uint* haveFx = &(state.have))
                len = (uint)(syncsearch(haveFx, strm.next_in, (uint)(strm.avail_in)));
            strm.avail_in -= (uint)(len);
            strm.next_in += len;
            strm.total_in += (uint)(len);
            if (state.have != 4)
                return (int)(-3);
            if ((state.flags) == (-1))
                state.wrap = (int)(0);
            else
                state.wrap &= (int)(~4);
            flags = (int)(state.flags);
            _in_ = (uint)(strm.total_in);
            _out_ = (uint)(strm.total_out);
            inflateReset(strm);
            strm.total_in = (uint)(_in_);
            strm.total_out = (uint)(_out_);
            state.flags = (int)(flags);
            state.mode = (inflate_mode.TYPE);
            return (int)(0);
        }

//#warning implement inflateCopy() as a clone

        //public static int inflateCopy(z_stream_s dest, z_stream_s source)
        //{
        //    inflate_state? state;
        //    inflate_state copy;
        //    byte* window;
        //    uint wsize = 0;
        //    if (((inflateStateCheck(source)) != 0) || ((dest) == (null)))
        //        return (int)(-2);
        //    state = (inflate_state)(source.state);
        //    copy = (inflate_state)(*((source).zalloc)((source).opaque, (uint)(1), (uint)(sizeof(inflate_state))));
        //    if ((copy) == (null))
        //        return (int)(-4);
        //    window = null;
        //    if (state.window != null)
        //    {
        //        window = (byte*)(*((source).zalloc)((source).opaque, (uint)(1U << state.wbits), (uint)(sizeof(unsignedchar))));
        //        if ((window) == (null))
        //        {
        //            *((source).zfree)((source).opaque, (void*)(copy));
        //            return (int)(-4);
        //        }
        //    }

        //    CRuntime.memcpy((void*)(dest), (void*)(source), (ulong)(sizeof(z_stream_s)));
        //    CRuntime.memcpy((void*)(copy), (void*)(state), (ulong)(sizeof(inflate_state)));
        //    copy.strm = dest;
        //    if (((state.lencode) >= (state.codes)) && ((state.lencode) <= (state.codes + (852 + 592) - 1)))
        //    {
        //        copy.lencode = copy.codes + (state.lencode - state.codes);
        //        copy.distcode = copy.codes + (state.distcode - state.codes);
        //    }

        //    copy.next = copy.codes + (state.next - state.codes);
        //    if (window != null)
        //    {
        //        wsize = (uint)(1U << state.wbits);
        //        CRuntime.memcpy(window, state.window, (ulong)(wsize));
        //    }

        //    copy.window = window;
        //    dest.state = (internal_state)(copy);
        //    return (int)(0);
        //}

        public static int inflateReset(z_stream_s strm)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            state.wsize = (uint)(0);
            state.whave = (uint)(0);
            state.wnext = (uint)(0);
            return (int)(inflateResetKeep(strm));
        }

        public static int inflateReset2(z_stream_s strm, int windowBits)
        {
            int wrap = 0;
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if ((windowBits) < (0))
            {
                wrap = (int)(0);
                windowBits = (int)(-windowBits);
            }
            else
            {
                wrap = (int)((windowBits >> 4) + 5);
                if ((windowBits) < (48))
                    windowBits &= (int)(15);
            }

            if (((windowBits) != 0) && (((windowBits) < (8)) || ((windowBits) > (15))))
                return (int)(-2);
            if ((state.windowArray != null) && (state.wbits != (uint)(windowBits)))
            {
                //*((strm).zfree)((strm).opaque, (void*)(state.window));
                state.windowArray.Free();
                state.window = null;
            }

            state.wrap = (int)(wrap);
            state.wbits = ((uint)(windowBits));
            return (int)(inflateReset(strm));
        }

        public static int inflatePrime(z_stream_s strm, int bits, int value)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if ((bits) < (0))
            {
                state.hold = (uint)(0);
                state.bits = (uint)(0);
                return (int)(0);
            }

            if (((bits) > (16)) || ((state.bits + (uint)(bits)) > (32)))
                return (int)(-2);
            value &= (int)((1L << bits) - 1);
            state.hold += (uint)((uint)(value) << (int)state.bits); //Nanook added (int)
            state.bits += ((uint)(bits));
            return (int)(0);
        }

        public static int inflateMark(z_stream_s strm)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-(1L << 16));
            state = (inflate_state)(strm.i_state!);
            return (int)((int)(((uint)(state.back)) << 16) + ((state.mode) == (inflate_mode.COPY) ? state.length : ((state.mode) == (inflate_mode.MATCH) ? state.was - state.length : 0)));
        }

        public static int inflateGetHeader(z_stream_s strm, gz_header_s* head)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if ((state.wrap & 2) == (0))
                return (int)(-2);
            state.head = head;
            head->done = (int)(0);
            return (int)(0);
        }

//#warning check zlibCompileFlags() this isn't required
        //public static uint zlibCompileFlags()
        //{
        //    uint flags = 0;
        //    flags = (uint)(0);
        //    switch ((int)(sizeof(uint)))
        //    {
        //        case 2:
        //            break;
        //        case 4:
        //            flags += (uint)(1);
        //            break;
        //        case 8:
        //            flags += (uint)(2);
        //            break;
        //        default:
        //            flags += (uint)(3);
        //    }

        //    switch ((int)(sizeof(uint)))
        //    {
        //        case 2:
        //            break;
        //        case 4:
        //            flags += (uint)(1 << 2);
        //            break;
        //        case 8:
        //            flags += (uint)(2 << 2);
        //            break;
        //        default:
        //            flags += (uint)(3 << 2);
        //    }

        //    switch ((int)(sizeof(void*)))
        //    {
        //        case 2:
        //            break;
        //        case 4:
        //            flags += (uint)(1 << 4);
        //            break;
        //        case 8:
        //            flags += (uint)(2 << 4);
        //            break;
        //        default:
        //            flags += (uint)(3 << 4);
        //            break; //Nanook
        //    }

        //    switch ((int)(sizeof(z_off_t)))
        //    {
        //        case 2:
        //            break;
        //        case 4:
        //            flags += (uint)(1 << 6);
        //            break;
        //        case 8:
        //            flags += (uint)(2 << 6);
        //            break;
        //        default:
        //            flags += (uint)(3 << 6);
        //            break; //Nanook
        //    }

        //    return (uint)(flags);
        //}



        public static int compress(byte* dest, uint* destLen, byte* source, uint sourceLen)
        {
            return (int)(compress2(dest, destLen, source, (uint)(sourceLen), (int)(-1)));
        }

        public static int compress2(byte* dest, uint* destLen, byte* source, uint sourceLen, int level)
        {
            z_stream_s stream = new z_stream_s();
            int err = 0;
            uint max = (uint)(0xFFFFFFFF); //Nanook was -1
            uint left = 0;
            left = (uint)(*destLen);
            *destLen = (uint)(0);
            //stream.zalloc = null; //Nanook not required
            //stream.zfree = null; //Nanook not required
            //stream.opaque = null; //Nanook not required
            err = (int)(deflateInit_((stream), (int)(level), "1.2.12", 0));
            if (err != 0)
                return (int)(err);
            stream.next_out = dest;
            stream.avail_out = (uint)(0);
            stream.next_in = source;
            stream.avail_in = (uint)(0);
            do
            {
                if ((stream.avail_out) == (0))
                {
                    stream.avail_out = (uint)((left) > (max) ? max : left);
                    left -= (uint)(stream.avail_out);
                }

                if ((stream.avail_in) == (0))
                {
                    stream.avail_in = (uint)((sourceLen) > (max) ? max : sourceLen);
                    sourceLen -= (uint)(stream.avail_in);
                }

                err = (int)(deflate(stream, (int)((sourceLen) != 0 ? 0 : 4)));
            }
            while ((err) == (0));
            *destLen = (uint)(stream.total_out);
            deflateEnd(stream);
            return (int)((err) == (1) ? 0 : err);
        }

        public static uint compressBound(uint sourceLen)
        {
            return (uint)(sourceLen + (sourceLen >> 12) + (sourceLen >> 14) + (sourceLen >> 25) + 13);
        }

        public static int uncompress(byte* dest, uint* destLen, byte* source, uint sourceLen)
        {
            return (int)(uncompress2(dest, destLen, source, &sourceLen));
        }

        public static int uncompress2(byte* dest, uint* destLen, byte* source, uint* sourceLen)
        {
            z_stream_s stream = new z_stream_s();
            int err = 0;
            uint max = (uint)(0xFFFFFFFF); //Nanook was -1
            uint len = 0; uint left = 0;
            byte* buf = stackalloc byte[1];
            len = (uint)(*sourceLen);
            if ((*destLen) != 0)
            {
                left = (uint)(*destLen);
                *destLen = (uint)(0);
            }
            else
            {
                left = (uint)(1);
                dest = buf;
            }

            stream.next_in = source;
            stream.avail_in = (uint)(0);
            //stream.zalloc = null;
            //stream.zfree = null;
            //stream.opaque = null;
            err = (int)(inflateInit_((stream), zlibVersion(), 0)); //Nanook removed (int)(sizeof(z_stream_s))));
            if (err != 0)
                return (int)(err);
            stream.next_out = dest;
            stream.avail_out = (uint)(0);
            do
            {
                if ((stream.avail_out) == (0))
                {
                    stream.avail_out = (uint)((left) > (max) ? max : left);
                    left -= (uint)(stream.avail_out);
                }

                if ((stream.avail_in) == (0))
                {
                    stream.avail_in = (uint)((len) > (max) ? max : len);
                    len -= (uint)(stream.avail_in);
                }

                err = (int)(inflate(stream, (int)(0)));
            }
            while ((err) == (0));
            *sourceLen -= (uint)(len + stream.avail_in);
            if (dest != buf)
                *destLen = (uint)(stream.total_out);
            else if (((stream.total_out) != 0) && ((err) == (-5)))
                left = (uint)(1);
            inflateEnd(stream);
            return (int)((err) == (1) ? 0 : (err) == (2) ? (-3) : ((err) == (-5)) && (left + stream.avail_out) != 0 ? (-3) : err);
        }

        public static uint adler32(uint adler, byte* buf, uint len)
        {
            return (uint)(adler32_z((uint)(adler), buf, (ulong)(len)));
        }

        public static uint adler32_z(uint adler, byte* buf, ulong len)
        {
            uint sum2 = 0;
            uint n = 0;
            sum2 = (uint)((adler >> 16) & 0xffff);
            adler &= (uint)(0xffff);
            if ((len) == (1))
            {
                adler += (uint)(buf[0]);
                if ((adler) >= (65521U))
                    adler -= (uint)(65521U);
                sum2 += (uint)(adler);
                if ((sum2) >= (65521U))
                    sum2 -= (uint)(65521U);
                return (uint)(adler | (sum2 << 16));
            }

            if ((buf) == (null))
                return (uint)(1L);
            if ((len) < (16))
            {
                while ((len--) != 0)
                {
                    adler += (uint)(*buf++);
                    sum2 += (uint)(adler);
                }

                if ((adler) >= (65521U))
                    adler -= (uint)(65521U);
                sum2 %= (uint)(65521U);
                return (uint)(adler | (sum2 << 16));
            }

            while ((len) >= (5552))
            {
                len -= (ulong)(5552);
                n = (uint)(5552 / 16);
                do
                {
                    {
                        adler += (uint)((buf)[0]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    buf += 16;
                }
                while ((--n) != 0);
                adler %= (uint)(65521U);
                sum2 %= (uint)(65521U);
            }

            if ((len) != 0)
            {
                while ((len) >= (16))
                {
                    len -= (ulong)(16);
                    {
                        adler += (uint)((buf)[0]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[0 + 4 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4 + 1]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4 + 2]);
                        sum2 += (uint)(adler);
                    }

                    {
                        adler += (uint)((buf)[8 + 4 + 2 + 1]);
                        sum2 += (uint)(adler);
                    }

                    buf += 16;
                }

                while ((len--) != 0)
                {
                    adler += (uint)(*buf++);
                    sum2 += (uint)(adler);
                }

                adler %= (uint)(65521U);
                sum2 %= (uint)(65521U);
            }

            return (uint)(adler | (sum2 << 16));
        }

        public static uint crc32(uint crc, byte* buf, uint len)
        {
            return (uint)(crc32_z((uint)(crc), buf, (ulong)(len)));
        }

        public static uint crc32_z(uint crc, byte* buf, ulong len)
        {
            if ((buf) == (null))
                return (uint)(0);
            crc ^= (uint)(0xffffffff);
            if ((len) >= (5 * 8 + 8 - 1))
            {
                ulong blks = 0;
                ulong* words;
                uint endian = 0;
                int k = 0;
                while (((len) != 0) && (((ulong)(buf) & (8 - 1)) != 0))
                {
                    len--;
                    crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                }

                blks = (ulong)(len / (5 * 8));
                len -= (ulong)(blks * 5 * 8);
                words = (ulong*)(buf);
                endian = (uint)(1);
                if ((*(byte*)(&endian)) != 0)
                {
                    uint crc0 = 0;
                    ulong word0 = 0;
                    uint crc1 = 0;
                    ulong word1 = 0;
                    uint crc2 = 0;
                    ulong word2 = 0;
                    uint crc3 = 0;
                    ulong word3 = 0;
                    uint crc4 = 0;
                    ulong word4 = 0;
                    crc0 = (uint)(crc);
                    crc1 = (uint)(0);
                    crc2 = (uint)(0);
                    crc3 = (uint)(0);
                    crc4 = (uint)(0);
                    while ((--blks) != 0)
                    {
                        word0 = (ulong)(crc0 ^ words[0]);
                        word1 = (ulong)(crc1 ^ words[1]);
                        word2 = (ulong)(crc2 ^ words[2]);
                        word3 = (ulong)(crc3 ^ words[3]);
                        word4 = (ulong)(crc4 ^ words[4]);
                        words += 5;
                        crc0 = (uint)(crc_braid_table[0][word0 & 0xff]);
                        crc1 = (uint)(crc_braid_table[0][word1 & 0xff]);
                        crc2 = (uint)(crc_braid_table[0][word2 & 0xff]);
                        crc3 = (uint)(crc_braid_table[0][word3 & 0xff]);
                        crc4 = (uint)(crc_braid_table[0][word4 & 0xff]);
                        for (k = (int)(1); (k) < (8); k++)
                        {
                            crc0 ^= (uint)(crc_braid_table[k][(word0 >> (k << 3)) & 0xff]);
                            crc1 ^= (uint)(crc_braid_table[k][(word1 >> (k << 3)) & 0xff]);
                            crc2 ^= (uint)(crc_braid_table[k][(word2 >> (k << 3)) & 0xff]);
                            crc3 ^= (uint)(crc_braid_table[k][(word3 >> (k << 3)) & 0xff]);
                            crc4 ^= (uint)(crc_braid_table[k][(word4 >> (k << 3)) & 0xff]);
                        }
                    }

                    crc = (uint)(crc_word((ulong)(crc0 ^ words[0])));
                    crc = (uint)(crc_word((ulong)(crc1 ^ words[1] ^ crc)));
                    crc = (uint)(crc_word((ulong)(crc2 ^ words[2] ^ crc)));
                    crc = (uint)(crc_word((ulong)(crc3 ^ words[3] ^ crc)));
                    crc = (uint)(crc_word((ulong)(crc4 ^ words[4] ^ crc)));
                    words += 5;
                }
                else
                {
                    ulong crc0 = 0;
                    ulong word0 = 0;
                    ulong comb = 0;
                    ulong crc1 = 0;
                    ulong word1 = 0;
                    ulong crc2 = 0;
                    ulong word2 = 0;
                    ulong crc3 = 0;
                    ulong word3 = 0;
                    ulong crc4 = 0;
                    ulong word4 = 0;
                    crc0 = (ulong)(byte_swap((ulong)(crc)));
                    crc1 = (ulong)(0);
                    crc2 = (ulong)(0);
                    crc3 = (ulong)(0);
                    crc4 = (ulong)(0);
                    while ((--blks) != 0)
                    {
                        word0 = (ulong)(crc0 ^ words[0]);
                        word1 = (ulong)(crc1 ^ words[1]);
                        word2 = (ulong)(crc2 ^ words[2]);
                        word3 = (ulong)(crc3 ^ words[3]);
                        word4 = (ulong)(crc4 ^ words[4]);
                        words += 5;
                        crc0 = (ulong)(crc_braid_big_table[0][word0 & 0xff]);
                        crc1 = (ulong)(crc_braid_big_table[0][word1 & 0xff]);
                        crc2 = (ulong)(crc_braid_big_table[0][word2 & 0xff]);
                        crc3 = (ulong)(crc_braid_big_table[0][word3 & 0xff]);
                        crc4 = (ulong)(crc_braid_big_table[0][word4 & 0xff]);
                        for (k = (int)(1); (k) < (8); k++)
                        {
                            crc0 ^= (ulong)(crc_braid_big_table[k][(word0 >> (k << 3)) & 0xff]);
                            crc1 ^= (ulong)(crc_braid_big_table[k][(word1 >> (k << 3)) & 0xff]);
                            crc2 ^= (ulong)(crc_braid_big_table[k][(word2 >> (k << 3)) & 0xff]);
                            crc3 ^= (ulong)(crc_braid_big_table[k][(word3 >> (k << 3)) & 0xff]);
                            crc4 ^= (ulong)(crc_braid_big_table[k][(word4 >> (k << 3)) & 0xff]);
                        }
                    }

                    comb = (ulong)(crc_word_big((ulong)(crc0 ^ words[0])));
                    comb = (ulong)(crc_word_big((ulong)(crc1 ^ words[1] ^ comb)));
                    comb = (ulong)(crc_word_big((ulong)(crc2 ^ words[2] ^ comb)));
                    comb = (ulong)(crc_word_big((ulong)(crc3 ^ words[3] ^ comb)));
                    comb = (ulong)(crc_word_big((ulong)(crc4 ^ words[4] ^ comb)));
                    words += 5;
                    crc = (uint)(byte_swap((ulong)(comb)));
                }

                buf = (byte*)(words);
            }

            while ((len) >= (8))
            {
                len -= (ulong)(8);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
            }

            while ((len) != 0)
            {
                len--;
                crc = (uint)((crc >> 8) ^ crc_table[(crc ^ *buf++) & 0xff]);
            }

            return (uint)(crc ^ 0xffffffff);
        }

        public static uint crc32_combine_op(uint crc1, uint crc2, uint op)
        {
            return (uint)(multmodp((uint)(op), (uint)(crc1)) ^ crc2);
        }

        public static int deflateInit_(z_stream_s strm, int level, string version, int stream_size)
        {
            return (int)(deflateInit2_(strm, (int)(level), (int)(8), (int)(15), (int)(8), (int)(0), version, (int)(stream_size)));
        }

        public static int inflateInit_(z_stream_s strm, string version, int stream_size)
        {
            return (int)(inflateInit2_(strm, (int)(15), version, (int)(stream_size)));
        }

        public static int deflateInit2_(z_stream_s strm, int level, int method, int windowBits, int memLevel, int strategy, string version, int stream_size)
        {
            internal_state? s;
            int wrap = (int)(1);
            if ((((version) == (null)) || (version[0] != deflateInit2__my_version[0]))) //Nanook removed streamSize check || (stream_size != sizeof(z_stream_s)))
            {
                return (int)(-6);
            }

            if ((strm) == (null))
                return (int)(-2);
            strm.msg = ""; //Nanook was null
            //if ((strm.zalloc) == null) //Nanook not using malloc
            //{
            //    strm.zalloc = zcalloc;
            //    strm.opaque = null;
            //}

            //if ((strm.zfree) == null) //Nanook not using free
            //    strm.zfree = zcfree;
            if ((level) == (-1))
                level = (int)(6);
            if ((windowBits) < (0))
            {
                wrap = (int)(0);
                windowBits = (int)(-windowBits);
            }
            else if ((windowBits) > (15))
            {
                wrap = (int)(2);
                windowBits -= (int)(16);
            }

            if (((((((((((memLevel) < (1)) || ((memLevel) > (9))) || (method != 8)) || ((windowBits) < (8))) || ((windowBits) > (15))) || ((level) < (0))) || ((level) > (9))) || ((strategy) < (0))) || ((strategy) > (4))) || (((windowBits) == (8)) && (wrap != 1)))
            {
                return (int)(-2);
            }

            if ((windowBits) == (8))
                windowBits = (int)(9);
            //s = (internal_state)(*((strm).zalloc)((strm).opaque, (uint)(1), (uint)(sizeof(internal_state))));
            s = new internal_state(); //Nanook
            if ((s) == (null))
                return (int)(-4);
            strm.state = s;
            s.strm = strm;
            s.status = (int)(42);
            s.wrap = (int)(wrap);
            s.gzhead = null;
            s.w_bits = ((uint)(windowBits));
            s.w_size = (uint)(1 << (int)s.w_bits); //Nanook added (int)
            s.w_mask = (uint)(s.w_size - 1);
            s.hash_bits = (uint)((uint)(memLevel) + 7);
            s.hash_size = (uint)(1 << (int)s.hash_bits); //Nanook added (int)
            s.hash_mask = (uint)(s.hash_size - 1);
            s.hash_shift = (uint)((s.hash_bits + 3 - 1) / 3);
            //Nanook s.window = (byte*)(*((strm).zalloc)((strm).opaque, (uint)(s.w_size), (uint)(2 * sizeof(byte))));
            s.windowArray = new UnsafeArray1D<byte>((int)s.w_size * 2 * sizeof(byte));
            s.window = (byte*)s.windowArray;
            //Nanook s.prev = (ushort*)(*((strm).zalloc)((strm).opaque, (uint)(s.w_size), (uint)(sizeof(ushort))));
            s.prevArray = new UnsafeArray1D<ushort>((int)s.w_size * sizeof(ushort));
            s.prev = (ushort*)s.prevArray;
            //Nanook s.head = (ushort*)(*((strm).zalloc)((strm).opaque, (uint)(s.hash_size), (uint)(sizeof(ushort))));
            s.headArray = new UnsafeArray1D<ushort>((int)s.hash_size * sizeof(ushort));
            s.head = (ushort*)s.headArray;
            s.high_water = (uint)(0);
            s.lit_bufsize = (uint)(1 << (memLevel + 6));
            //Nanook s.pending_buf = (byte*)(*((strm).zalloc)((strm).opaque, (uint)(s.lit_bufsize), (uint)(4)));
            s.pending_bufArray = new UnsafeArray1D<byte>((int)s.lit_bufsize * 4);
            s.pending_buf = (byte*)s.pending_bufArray;
            s.pending_buf_size = (uint)(s.lit_bufsize * 4);
            if (((((s.window) == (null)) || ((s.prev) == (null))) || ((s.head) == (null))) || ((s.pending_buf) == (null)))
            {
                s.status = (int)(666);
                strm.msg = z_errmsg[2 - (-4)];
                deflateEnd(strm);
                return (int)(-4);
            }

            s.sym_buf = s.pending_buf + s.lit_bufsize;
            s.sym_end = (uint)((s.lit_bufsize - 1) * 3);
            s.level = (int)(level);
            s.strategy = (int)(strategy);
            s.method = ((byte)(method));
            return (int)(deflateReset(strm));
        }

        public static int inflateInit2_(z_stream_s strm, int windowBits, string version, int stream_size)
        {
            int ret = 0;
            inflate_state? state;
            if ((((version) == (null)) || (version[0] != zlibVersion()[0]))) //Nanook removed || (stream_size != (int)(sizeof(z_stream_s))))
                return (int)(-6);
            if ((strm) == (null))
                return (int)(-2);
            strm.msg = null;
            //if ((strm.zalloc) == null) //Nanook not using malloc
            //{
            //    strm.zalloc = zcalloc;
            //    strm.opaque = null;
            //}

            //if ((strm.zfree) == null) //Nanook not using malloc
            //    strm.zfree = zcfree;
            //state = (inflate_state)(*((strm).zalloc)((strm).opaque, (uint)(1), (uint)(sizeof(inflate_state))));
            state = new inflate_state(); //Nanook
            if ((state) == (null))
                return (int)(-4);
            strm.i_state = state; //Nanook (internal_state)(state);
            state.strm = strm;
            state.window = null;
            state.mode = (inflate_mode.HEAD);
            ret = (int)(inflateReset2(strm, (int)(windowBits)));
            if (ret != 0)
            {
                //*((strm).zfree)((strm).opaque, (void*)(state)); //Nanook
                strm.i_state = null;
            }

            return (int)(ret);
        }

        public static uint adler32_combine(uint adler1, uint adler2, int len2)
        {
            return (uint)(adler32_combine_((uint)(adler1), (uint)(adler2), (long)(len2)));
        }

        public static uint crc32_combine(uint crc1, uint crc2, int len2)
        {
            return (uint)(crc32_combine64((uint)(crc1), (uint)(crc2), (long)(len2)));
        }

        public static uint crc32_combine_gen(int len2)
        {
            return (uint)(crc32_combine_gen64((long)(len2)));
        }

        public static string zError(int err) //Nanook was sbyte*
        {
            return z_errmsg[2 - (err)];
        }

        public static int inflateSyncPoint(z_stream_s strm)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            return (((state.mode) == (inflate_mode.STORED)) && ((state.bits) == (0)) ? 1 : 0);
        }

        public static uint[] get_crc_table()
        {
            return crc_table;
        }

        public static int inflateUndermine(z_stream_s strm, int subvert)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            //(void)(subvert); //Nanook
            state.sane = (int)(1);
            return (int)(-3);
        }

        public static int inflateValidate(z_stream_s strm, int check)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            if (((check) != 0) && ((state.wrap) != 0))
                state.wrap |= (int)(4);
            else
                state.wrap &= (int)(~4);
            return (int)(0);
        }

        public static uint inflateCodesUsed(z_stream_s strm)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (uint)(0xFFFFFFFF); //Nanook was -1
            state = (inflate_state)(strm.i_state!);
            return (uint)(state.next - state.codes);
        }

        public static int inflateResetKeep(z_stream_s strm)
        {
            inflate_state? state;
            if ((inflateStateCheck(strm)) != 0)
                return (int)(-2);
            state = (inflate_state)(strm.i_state!);
            strm.total_in = (uint)(strm.total_out = (uint)(state.total = (uint)(0)));
            strm.msg = null;
            if ((state.wrap) != 0)
                strm.adler = (uint)(state.wrap & 1);
            state.mode = (inflate_mode.HEAD);
            state.last = (int)(0);
            state.havedict = (int)(0);
            state.flags = (int)(-1);
            state.dmax = (uint)(32768U);
            state.head = null;
            state.hold = (uint)(0);
            state.bits = (uint)(0);
            state.lencode = state.distcode = state.next = state.codes;
            state.sane = (int)(1);
            state.back = (int)(-1);
            return (int)(0);
        }

        public static int deflateResetKeep(z_stream_s strm)
        {
            internal_state? s;
            if ((deflateStateCheck(strm)) != 0)
            {
                return (int)(-2);
            }

            strm.total_in = (uint)(strm.total_out = (uint)(0));
            strm.msg = ""; //Nanook was null
            strm.data_type = (int)(2);
            s = strm.state;
            s!.pending = (uint)(0);
            s.pending_out = s.pending_buf;
            if ((s.wrap) < (0))
            {
                s.wrap = (int)(-s.wrap);
            }

            s.status = (int)((s.wrap) == (2) ? 57 : 42);
            strm.adler = (uint)((s.wrap) == (2) ? crc32((uint)(0L), null, (uint)(0)) : adler32((uint)(0L), null, (uint)(0)));
            s.last_flush = (int)(-2);
            _tr_init(s);
            return (int)(0);
        }

        //Nanook all done through UnsafeArray
        //public static void* zcalloc(void* opaque, uint items, uint size)
        //{
        //    //(void)(opaque);
        //    return (sizeof(uint)) > (2) ? CRuntime.malloc((ulong)(items * size)) : calloc((ulong)(items), (ulong)(size));
        //}

        //Nanook all done through UnsafeArray
        //public static void zcfree(void* opaque, void* ptr)
        //{
        //    //(void)(opaque);
        //    CRuntime.free(ptr);
        //}

        public static uint adler32_combine_(uint adler1, uint adler2, long len2)
        {
            uint sum1 = 0;
            uint sum2 = 0;
            uint rem = 0;
            if ((len2) < (0))
                return (uint)(0xffffffffUL);
            len2 %= (long)(65521U);
            rem = ((uint)(len2));
            sum1 = (uint)(adler1 & 0xffff);
            sum2 = (uint)(rem * sum1);
            sum2 %= (uint)(65521U);
            sum1 += (uint)((adler2 & 0xffff) + 65521U - 1);
            sum2 += (uint)(((adler1 >> 16) & 0xffff) + ((adler2 >> 16) & 0xffff) + 65521U - rem);
            if ((sum1) >= (65521U))
                sum1 -= (uint)(65521U);
            if ((sum1) >= (65521U))
                sum1 -= (uint)(65521U);
            if ((sum2) >= (65521U << 1))
                sum2 -= (uint)(65521U << 1);
            if ((sum2) >= (65521U))
                sum2 -= (uint)(65521U);
            return (uint)(sum1 | (sum2 << 16));
        }

        public static uint adler32_combine64(uint adler1, uint adler2, long len2)
        {
            return (uint)(adler32_combine_((uint)(adler1), (uint)(adler2), (long)(len2)));
        }

        public static uint multmodp(uint a, uint b)
        {
            uint m = 0; uint  p  =  0 ;
            m = (uint)((uint)(1) << 31);
            p = (uint)(0);
            for (;;)
            {
                if ((a & m) != 0)
                {
                    p ^= (uint)(b);
                    if ((a & (m - 1)) == (0))
                        break;
                }

                m >>= 1;
                b = (uint)((b & 1) != 0 ? (b >> 1) ^ 0xedb88320 : b >> 1);
            }

            return (uint)(p);
        }

        public static uint x2nmodp(long n, uint k)
        {
            uint p = 0;
            p = (uint)((uint)(1) << 31);
            while ((n) != 0)
            {
                if ((n & 1) != 0)
                    p = (uint)(multmodp((uint)(x2n_table[k & 31]), (uint)(p)));
                n >>= 1;
                k++;
            }

            return (uint)(p);
        }

        public static ulong byte_swap(ulong word)
        {
            return (ulong)((word & 0xff00000000000000) >> 56 | (word & 0xff000000000000) >> 40 | (word & 0xff0000000000) >> 24 | (word & 0xff00000000) >> 8 | (word & 0xff000000) << 8 | (word & 0xff0000) << 24 | (word & 0xff00) << 40 | (word & 0xff) << 56);
        }

        public static uint crc_word(ulong data)
        {
            int k = 0;
            for (k = (int)(0); (k) < (8); k++)
            {
                data = (ulong)((data >> 8) ^ crc_table[data & 0xff]);
            }

            return (uint)(data);
        }

        public static ulong crc_word_big(ulong data)
        {
            int k = 0;
            for (k = (int)(0); (k) < (8); k++)
            {
                data = (ulong)((data << 8) ^ crc_big_table[(data >> ((8 - 1) << 3)) & 0xff]);
            }

            return (ulong)(data);
        }

        public static uint crc32_combine64(uint crc1, uint crc2, long len2)
        {
            return (uint)(multmodp((uint)(x2nmodp((long)(len2), (uint)(3))), (uint)(crc1)) ^ crc2);
        }

        public static uint crc32_combine_gen64(long len2)
        {
            return (uint)(x2nmodp((long)(len2), (uint)(3)));
        }

        public static void _tr_init(internal_state s)
        {
            tr_static_init();
            s.l_desc.dyn_tree = s.dyn_ltreeArray.Data;
            s.l_desc.stat_desc = static_l_desc; //Nanook was &static_l_desc;
            s.d_desc.dyn_tree = s.dyn_dtreeArray.Data;
            s.d_desc.stat_desc = static_d_desc; //Nanook was &static_d_desc;
            s.bl_desc.dyn_tree = s.bl_treeArray.Data;
            s.bl_desc.stat_desc = static_bl_desc; //Nanook was &static_bl_desc;
            s.bi_buf = (ushort)(0);
            s.bi_valid = (int)(0);
            init_block(s);
        }

        public static int _tr_tally(internal_state s, uint dist, uint lc)
        {
            s.sym_buf[s.sym_next++] = (byte)(dist);
            s.sym_buf[s.sym_next++] = (byte)(dist >> 8);
            s.sym_buf[s.sym_next++] = (byte)(lc);
            if ((dist) == (0))
            {
                s.dyn_ltree[lc].fc.freq++;
            }
            else
            {
                s.matches++;
                dist--;
                s.dyn_ltree[_length_code[lc] + 256 + 1].fc.freq++;
                s.dyn_dtree[((dist) < (256) ? _dist_code[dist] : _dist_code[256 + ((dist) >> 7)])].fc.freq++;
            }

            return (((s.sym_next) == (s.sym_end)) ? 1 : 0);
        }

        public static void _tr_flush_block(internal_state s, sbyte* buf, uint stored_len, int last)
        {
            uint opt_lenb = 0; uint  static_lenb  =  0 ;
            int max_blindex = (int)(0);
            if ((s.level) > (0))
            {
                if ((s.strm!.data_type) == (2))
                    s.strm.data_type = (int)(detect_data_type(s));
                build_tree(s, s.l_desc); //Nanook was build_tree(s, (&(s.l_desc)));
                build_tree(s, s.d_desc); //Nanook was build_tree(s, (&(s.d_desc)));
                max_blindex = (int)(build_bl_tree(s));
                opt_lenb = (uint)((s.opt_len + 3 + 7) >> 3);
                static_lenb = (uint)((s.static_len + 3 + 7) >> 3);
                if ((static_lenb) <= (opt_lenb))
                    opt_lenb = (uint)(static_lenb);
            }
            else
            {
                opt_lenb = (uint)(static_lenb = (uint)(stored_len + 5));
            }

            if (((stored_len + 4) <= (opt_lenb)) && (buf != null))
            {
                _tr_stored_block(s, buf, (uint)(stored_len), (int)(last));
            }
            else if (((s.strategy) == (4)) || ((static_lenb) == (opt_lenb)))
            {
                {
                    int len = (int)(3);
                    if ((s.bi_valid) > (16 - len))
                    {
                        int val = (int)((1 << 1) + last);
                        s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }

                        s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                        s.bi_valid += (int)(len - 16);
                    }
                    else
                    {
                        s.bi_buf |= (ushort)((ushort)((1 << 1) + last) << s.bi_valid);
                        s.bi_valid += (int)(len);
                    }
                }

                compress_block(s, static_ltree, static_dtree);
            }
            else
            {
                {
                    int len = (int)(3);
                    if ((s.bi_valid) > (16 - len))
                    {
                        int val = (int)((2 << 1) + last);
                        s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                        {
                            {
                                s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                            }

                            {
                                s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                            }
                        }

                        s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                        s.bi_valid += (int)(len - 16);
                    }
                    else
                    {
                        s.bi_buf |= (ushort)((ushort)((2 << 1) + last) << s.bi_valid);
                        s.bi_valid += (int)(len);
                    }
                }

                send_all_trees(s, (int)(s.l_desc.max_code + 1), (int)(s.d_desc.max_code + 1), (int)(max_blindex + 1));
                compress_block(s, s.dyn_ltreeArray.Data, s.dyn_dtreeArray.Data);
            }

            init_block(s);
            if ((last) != 0)
            {
                bi_windup(s);
            }
        }

        public static void _tr_flush_bits(internal_state s)
        {
            bi_flush(s);
        }

        public static void _tr_align(internal_state s)
        {
            {
                int len = (int)(3);
                if ((s.bi_valid) > (16 - len))
                {
                    int val = (int)(1 << 1);
                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }
                    }

                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                    s.bi_valid += (int)(len - 16);
                }
                else
                {
                    s.bi_buf |= (ushort)((ushort)(1 << 1) << s.bi_valid);
                    s.bi_valid += (int)(len);
                }
            }

            {
                int len = (int)(static_ltree[256].dl.len);
                if ((s.bi_valid) > (16 - len))
                {
                    int val = (int)(static_ltree[256].fc.code);
                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }
                    }

                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                    s.bi_valid += (int)(len - 16);
                }
                else
                {
                    s.bi_buf |= (ushort)((static_ltree[256].fc.code) << s.bi_valid);
                    s.bi_valid += (int)(len);
                }
            }

            bi_flush(s);
        }

        public static void _tr_stored_block(internal_state s, sbyte* buf, uint stored_len, int last)
        {
            {
                int len = (int)(3);
                if ((s.bi_valid) > (16 - len))
                {
                    int val = (int)((0 << 1) + last);
                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }
                    }

                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                    s.bi_valid += (int)(len - 16);
                }
                else
                {
                    s.bi_buf |= (ushort)((ushort)((0 << 1) + last) << s.bi_valid);
                    s.bi_valid += (int)(len);
                }
            }

            bi_windup(s);
            {
                {
                    s.pending_buf[s.pending++] = ((byte)(((ushort)(stored_len)) & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)(((ushort)(stored_len)) >> 8));
                }
            }

            {
                {
                    s.pending_buf[s.pending++] = ((byte)(((ushort)(~stored_len)) & 0xff));
                }

                {
                    s.pending_buf[s.pending++] = ((byte)(((ushort)(~stored_len)) >> 8));
                }
            }

            if ((stored_len) != 0)
                CRuntime.memcpy(s.pending_buf + s.pending, (byte*)(buf), (ulong)(stored_len));
            s.pending += (uint)(stored_len);
        }

        public static void tr_static_init()
        {
        }

        public static void init_block(internal_state s)
        {
            int n = 0;
            for (n = (int)(0); (n) < (256 + 1 + 29); n++)
            {
                s.dyn_ltree[n].fc.freq = (ushort)(0);
            }

            for (n = (int)(0); (n) < (30); n++)
            {
                s.dyn_dtree[n].fc.freq = (ushort)(0);
            }

            for (n = (int)(0); (n) < (19); n++)
            {
                s.bl_tree[n].fc.freq = (ushort)(0);
            }

            s.dyn_ltree[256].fc.freq = (ushort)(1);
            s.opt_len = (uint)(s.static_len = (uint)(0L));
            s.sym_next = (uint)(s.matches = (uint)(0));
        }

        public static void pqdownheap(internal_state s, ct_data_s[] tree, int k)
        {
            int v = (int)(s.heap[k]);
            int j = (int)(k << 1);
            while ((j) <= (s.heap_len))
            {
                if (((j) < (s.heap_len)) && (((tree[s.heap[j + 1]].fc.freq) < (tree[s.heap[j]].fc.freq)) || (((tree[s.heap[j + 1]].fc.freq) == (tree[s.heap[j]].fc.freq)) && ((s.depth[s.heap[j + 1]]) <= (s.depth[s.heap[j]])))))
                {
                    j++;
                }

                if ((((tree[v].fc.freq) < (tree[s.heap[j]].fc.freq)) || (((tree[v].fc.freq) == (tree[s.heap[j]].fc.freq)) && ((s.depth[v]) <= (s.depth[s.heap[j]])))))
                    break;
                s.heap[k] = (int)(s.heap[j]);
                k = (int)(j);
                j <<= 1;
            }

            s.heap[k] = (int)(v);
        }

        public static void gen_bitlen(internal_state s, tree_desc_s desc)
        {
            ct_data_s[] tree = desc.dyn_tree!;
            int max_code = (int)(desc.max_code);
            ct_data_s[] stree = desc.stat_desc.static_tree;
            int[] extra = desc.stat_desc.extra_bits;
            int _base_ = (int)(desc.stat_desc.extra_base);
            int max_length = (int)(desc.stat_desc.max_length);
            int h = 0;
            int n = 0; int  m  =  0 ;
            int bits = 0;
            int xbits = 0;
            ushort f = 0;
            int overflow = (int)(0);
            for (bits = (int)(0); (bits) <= (15); bits++)
            {
                s.bl_count[bits] = (ushort)(0);
            }

            tree[s.heap[s.heap_max]].dl.len = (ushort)(0);
            for (h = (int)(s.heap_max + 1); (h) < (2 * (256 + 1 + 29) + 1); h++)
            {
                n = (int)(s.heap[h]);
                bits = (int)(tree[tree[n].dl.dad].dl.len + 1);
                if ((bits) > (max_length))
                {
                    bits = (int)(max_length); //Nanook ,
                    overflow++;
                }
                tree[n].dl.len = ((ushort)(bits));
                if ((n) > (max_code))
                    continue;
                s.bl_count[bits]++;
                xbits = (int)(0);
                if ((n) >= (_base_))
                    xbits = (int)(extra[n - _base_]);
                f = (ushort)(tree[n].fc.freq);
                s.opt_len += (uint)((uint)(f) * (uint)(bits + xbits));
                if ((stree) != null)
                    s.static_len += (uint)((uint)(f) * (uint)(stree[n].dl.len + xbits));
            }

            if ((overflow) == (0))
                return;
            do
            {
                bits = (int)(max_length - 1);
                while ((s.bl_count[bits]) == (0))
                {
                    bits--;
                }

                s.bl_count[bits]--;
                s.bl_count[bits + 1] += (ushort)(2);
                s.bl_count[max_length]--;
                overflow -= (int)(2);
            }
            while ((overflow) > (0));
            for (bits = (int)(max_length); bits != 0; bits--)
            {
                n = (int)(s.bl_count[bits]);
                while (n != 0)
                {
                    m = (int)(s.heap[--h]);
                    if ((m) > (max_code))
                        continue;
                    if ((uint)(tree[m].dl.len) != (uint)(bits))
                    {
                        s.opt_len += (uint)(((uint)(bits) - tree[m].dl.len) * tree[m].fc.freq);
                        tree[m].dl.len = ((ushort)(bits));
                    }

                    n--;
                }
            }
        }

        public static void gen_codes(ct_data_s[] tree, int max_code, ushort* bl_count)
        {
            ushort* next_code = stackalloc ushort[16];
            uint code = (uint)(0);
            int bits = 0;
            int n = 0;
            for (bits = (int)(1); (bits) <= (15); bits++)
            {
                code = (uint)((code + bl_count[bits - 1]) << 1);
                next_code[bits] = ((ushort)(code));
            }

            for (n = (int)(0); (n) <= (max_code); n++)
            {
                int len = (int)(tree[n].dl.len);
                if ((len) == (0))
                    continue;
                tree[n].fc.code = ((ushort)(bi_reverse((uint)(next_code[len]++), (int)(len))));
            }
        }

        public static void build_tree(internal_state s, tree_desc_s desc)
        {
            ct_data_s[] tree = desc.dyn_tree!;
            ct_data_s[] stree = desc.stat_desc.static_tree;
            int elems = (int)(desc.stat_desc.elems);
            int n = 0; int  m  =  0 ;
            int max_code = (int)(-1);
            int node = 0;
            s.heap_len = (int)(0); //Nanook ,
            s.heap_max  =  (int)(2 * (256 + 1 + 29) + 1);
            for (n = (int)(0); (n) < (elems); n++)
            {
                if (tree[n].fc.freq != 0)
                {
                    s.heap[++(s.heap_len)] = (int)(max_code = (int)(n));
                    s.depth[n] = (byte)(0);
                }
                else
                {
                    tree[n].dl.len = (ushort)(0);
                }
            }

            while ((s.heap_len) < (2))
            {
                node = (int)(s.heap[++(s.heap_len)] = (int)((max_code) < (2) ? ++max_code : 0));
                tree[node].fc.freq = (ushort)(1);
                s.depth[node] = (byte)(0);
                s.opt_len--;
                if ((stree) != null)
                    s.static_len -= (uint)(stree[node].dl.len);
            }

            desc.max_code = (int)(max_code);
            for (n = (int)(s.heap_len / 2); (n) >= (1); n--)
            {
                pqdownheap(s, tree, (int)(n));
            }

            node = (int)(elems);
            do
            {
                {
                    n = (int)(s.heap[1]);
                    s.heap[1] = (int)(s.heap[s.heap_len--]);
                    pqdownheap(s, tree, (int)(1));
                }

                m = (int)(s.heap[1]);
                s.heap[--(s.heap_max)] = (int)(n);
                s.heap[--(s.heap_max)] = (int)(m);
                tree[node].fc.freq = (ushort)(tree[n].fc.freq + tree[m].fc.freq);
                s.depth[node] = ((byte)(((s.depth[n]) >= (s.depth[m]) ? s.depth[n] : s.depth[m]) + 1));
                tree[n].dl.dad = (ushort)(tree[m].dl.dad = ((ushort)(node)));

                s.heap[1] = (int)(node++);
                pqdownheap(s, tree, (int)(1));
            }
            while ((s.heap_len) >= (2));
            s.heap[--(s.heap_max)] = (int)(s.heap[1]);
            gen_bitlen(s, desc);
            gen_codes(tree, (int)(max_code), s.bl_count);
        }

        public static void scan_tree(internal_state s, ct_data_s* tree, int max_code)
        {
            int n = 0;
            int prevlen = (int)(-1);
            int curlen = 0;
            int nextlen = (int)(tree[0].dl.len);
            int count = (int)(0);
            int max_count = (int)(7);
            int min_count = (int)(4);
            if ((nextlen) == (0))
            {
                max_count = (int)(138); //Nanook ,
                min_count  =  ( int ) ( 3 ) ;
            }
            tree[max_code + 1].dl.len = ((ushort)(0xffff));
            for (n = (int)(0); (n) <= (max_code); n++)
            {
                curlen = (int)(nextlen);
                nextlen = (int)(tree[n + 1].dl.len);
                if (((++count) < (max_count)) && ((curlen) == (nextlen)))
                {
                    continue;
                }
                else if ((count) < (min_count))
                {
                    s.bl_tree[curlen].fc.freq += (ushort)(count);
                }
                else if (curlen != 0)
                {
                    if (curlen != prevlen)
                        s.bl_tree[curlen].fc.freq++;
                    s.bl_tree[16].fc.freq++;
                }
                else if ((count) <= (10))
                {
                    s.bl_tree[17].fc.freq++;
                }
                else
                {
                    s.bl_tree[18].fc.freq++;
                }

                count = (int)(0);
                prevlen = (int)(curlen);
                if ((nextlen) == (0))
                {
                    max_count = (int)(138); //Nanook ,
                    min_count = (int)(3);
                }
                else if ((curlen) == (nextlen))
                {
                    max_count = (int)(6); //Nanook ,
                    min_count = (int)(3);
                }
                else
                {
                    max_count = (int)(7); //Nanook ,
                    min_count = (int)(4);
                }
            }
        }

        public static void send_tree(internal_state s, ct_data_s* tree, int max_code)
        {
            int n = 0;
            int prevlen = (int)(-1);
            int curlen = 0;
            int nextlen = (int)(tree[0].dl.len);
            int count = (int)(0);
            int max_count = (int)(7);
            int min_count = (int)(4);
            if ((nextlen) == (0))
            {
                max_count = (int)(138); //Nanook ,
                min_count = (int)(3);
            }
            for (n = (int)(0); (n) <= (max_code); n++)
            {
                curlen = (int)(nextlen);
                nextlen = (int)(tree[n + 1].dl.len);
                if (((++count) < (max_count)) && ((curlen) == (nextlen)))
                {
                    continue;
                }
                else if ((count) < (min_count))
                {
                    do
                    {
                        {
                            int len = (int)(s.bl_tree[curlen].dl.len);
                            if ((s.bi_valid) > (16 - len))
                            {
                                int val = (int)(s.bl_tree[curlen].fc.code);
                                s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                                {
                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                    }

                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                    }
                                }

                                s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                                s.bi_valid += (int)(len - 16);
                            }
                            else
                            {
                                s.bi_buf |= (ushort)((s.bl_tree[curlen].fc.code) << s.bi_valid);
                                s.bi_valid += (int)(len);
                            }
                        }
                    }
                    while (--count != 0);
                }
                else if (curlen != 0)
                {
                    if (curlen != prevlen)
                    {
                        {
                            int len = (int)(s.bl_tree[curlen].dl.len);
                            if ((s.bi_valid) > (16 - len))
                            {
                                int val = (int)(s.bl_tree[curlen].fc.code);
                                s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                                {
                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                    }

                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                    }
                                }

                                s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                                s.bi_valid += (int)(len - 16);
                            }
                            else
                            {
                                s.bi_buf |= (ushort)((s.bl_tree[curlen].fc.code) << s.bi_valid);
                                s.bi_valid += (int)(len);
                            }
                        }

                        count--;
                    }

                    {
                        int len = (int)(s.bl_tree[16].dl.len);
                        if ((s.bi_valid) > (16 - len))
                        {
                            int val = (int)(s.bl_tree[16].fc.code);
                            s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                            {
                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                }

                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                }
                            }

                            s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                            s.bi_valid += (int)(len - 16);
                        }
                        else
                        {
                            s.bi_buf |= (ushort)((s.bl_tree[16].fc.code) << s.bi_valid);
                            s.bi_valid += (int)(len);
                        }
                    }

                    {
                        int len = (int)(2);
                        if ((s.bi_valid) > (16 - len))
                        {
                            int val = (int)(count - 3);
                            s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                            {
                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                }

                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                }
                            }

                            s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                            s.bi_valid += (int)(len - 16);
                        }
                        else
                        {
                            s.bi_buf |= (ushort)((ushort)(count - 3) << s.bi_valid);
                            s.bi_valid += (int)(len);
                        }
                    }
                }
                else if ((count) <= (10))
                {
                    {
                        int len = (int)(s.bl_tree[17].dl.len);
                        if ((s.bi_valid) > (16 - len))
                        {
                            int val = (int)(s.bl_tree[17].fc.code);
                            s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                            {
                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                }

                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                }
                            }

                            s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                            s.bi_valid += (int)(len - 16);
                        }
                        else
                        {
                            s.bi_buf |= (ushort)((s.bl_tree[17].fc.code) << s.bi_valid);
                            s.bi_valid += (int)(len);
                        }
                    }

                    {
                        int len = (int)(3);
                        if ((s.bi_valid) > (16 - len))
                        {
                            int val = (int)(count - 3);
                            s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                            {
                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                }

                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                }
                            }

                            s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                            s.bi_valid += (int)(len - 16);
                        }
                        else
                        {
                            s.bi_buf |= (ushort)((ushort)(count - 3) << s.bi_valid);
                            s.bi_valid += (int)(len);
                        }
                    }
                }
                else
                {
                    {
                        int len = (int)(s.bl_tree[18].dl.len);
                        if ((s.bi_valid) > (16 - len))
                        {
                            int val = (int)(s.bl_tree[18].fc.code);
                            s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                            {
                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                }

                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                }
                            }

                            s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                            s.bi_valid += (int)(len - 16);
                        }
                        else
                        {
                            s.bi_buf |= (ushort)((s.bl_tree[18].fc.code) << s.bi_valid);
                            s.bi_valid += (int)(len);
                        }
                    }

                    {
                        int len = (int)(7);
                        if ((s.bi_valid) > (16 - len))
                        {
                            int val = (int)(count - 11);
                            s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                            {
                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                }

                                {
                                    s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                }
                            }

                            s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                            s.bi_valid += (int)(len - 16);
                        }
                        else
                        {
                            s.bi_buf |= (ushort)((ushort)(count - 11) << s.bi_valid);
                            s.bi_valid += (int)(len);
                        }
                    }
                }

                count = (int)(0);
                prevlen = (int)(curlen);
                if ((nextlen) == (0))
                {
                    max_count = (int)(138); //Nanook ,
                    min_count = (int)(3);
                }
                else if ((curlen) == (nextlen))
                {
                    max_count = (int)(6); //Nanook ,
                    min_count = (int)(3);
                }
                else
                {
                    max_count = (int)(7); //Nanook ,
                    min_count = (int)(4);
                }
            }
        }

        public static int build_bl_tree(internal_state s)
        {
            int max_blindex = 0;
            scan_tree(s, s.dyn_ltree, (int)(s.l_desc.max_code));
            scan_tree(s, s.dyn_dtree, (int)(s.d_desc.max_code));
            build_tree(s, s.bl_desc); //Nanook was (&(s.bl_desc)));
            for (max_blindex = (int)(19 - 1); (max_blindex) >= (3); max_blindex--)
            {
                if (s.bl_tree[bl_order[max_blindex]].dl.len != 0)
                    break;
            }

            s.opt_len += (uint)(3 * ((uint)(max_blindex) + 1) + 5 + 5 + 4);
            return (int)(max_blindex);
        }

        public static void send_all_trees(internal_state s, int lcodes, int dcodes, int blcodes)
        {
            int rank = 0;
            {
                int len = (int)(5);
                if ((s.bi_valid) > (16 - len))
                {
                    int val = (int)(lcodes - 257);
                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }
                    }

                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                    s.bi_valid += (int)(len - 16);
                }
                else
                {
                    s.bi_buf |= (ushort)((ushort)(lcodes - 257) << s.bi_valid);
                    s.bi_valid += (int)(len);
                }
            }

            {
                int len = (int)(5);
                if ((s.bi_valid) > (16 - len))
                {
                    int val = (int)(dcodes - 1);
                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }
                    }

                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                    s.bi_valid += (int)(len - 16);
                }
                else
                {
                    s.bi_buf |= (ushort)((ushort)(dcodes - 1) << s.bi_valid);
                    s.bi_valid += (int)(len);
                }
            }

            {
                int len = (int)(4);
                if ((s.bi_valid) > (16 - len))
                {
                    int val = (int)(blcodes - 4);
                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }
                    }

                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                    s.bi_valid += (int)(len - 16);
                }
                else
                {
                    s.bi_buf |= (ushort)((ushort)(blcodes - 4) << s.bi_valid);
                    s.bi_valid += (int)(len);
                }
            }

            for (rank = (int)(0); (rank) < (blcodes); rank++)
            {
                {
                    int len = (int)(3);
                    if ((s.bi_valid) > (16 - len))
                    {
                        int val = (int)(s.bl_tree[bl_order[rank]].dl.len);
                        s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                        {
                            {
                                s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                            }

                            {
                                s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                            }
                        }

                        s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                        s.bi_valid += (int)(len - 16);
                    }
                    else
                    {
                        s.bi_buf |= (ushort)((s.bl_tree[bl_order[rank]].dl.len) << s.bi_valid);
                        s.bi_valid += (int)(len);
                    }
                }
            }

            send_tree(s, s.dyn_ltree, (int)(lcodes - 1));
            send_tree(s, s.dyn_dtree, (int)(dcodes - 1));
        }

        public static void compress_block(internal_state s, ct_data_s[] ltree, ct_data_s[] dtree)
        {
            uint dist = 0;
            int lc = 0;
            uint sx = (uint)(0);
            uint code = 0;
            int extra = 0;
            if (s.sym_next != 0)
                do
                {
                    dist = (uint)(s.sym_buf[sx++] & 0xff);
                    dist += (uint)((uint)(s.sym_buf[sx++] & 0xff) << 8);
                    lc = (int)(s.sym_buf[sx++]);
                    if ((dist) == (0))
                    {
                        {
                            int len = (int)(ltree[lc].dl.len);
                            if ((s.bi_valid) > (16 - len))
                            {
                                int val = (int)(ltree[lc].fc.code);
                                s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                                {
                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                    }

                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                    }
                                }

                                s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                                s.bi_valid += (int)(len - 16);
                            }
                            else
                            {
                                s.bi_buf |= (ushort)((ltree[lc].fc.code) << s.bi_valid);
                                s.bi_valid += (int)(len);
                            }
                        }
                    }
                    else
                    {
                        code = (uint)(_length_code[lc]);
                        {
                            int len = (int)(ltree[code + 256 + 1].dl.len);
                            if ((s.bi_valid) > (16 - len))
                            {
                                int val = (int)(ltree[code + 256 + 1].fc.code);
                                s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                                {
                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                    }

                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                    }
                                }

                                s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                                s.bi_valid += (int)(len - 16);
                            }
                            else
                            {
                                s.bi_buf |= (ushort)((ltree[code + 256 + 1].fc.code) << s.bi_valid);
                                s.bi_valid += (int)(len);
                            }
                        }

                        extra = (int)(extra_lbits[code]);
                        if (extra != 0)
                        {
                            lc -= (int)(base_length[code]);
                            {
                                int len = (int)(extra);
                                if ((s.bi_valid) > (16 - len))
                                {
                                    int val = (int)(lc);
                                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                                    {
                                        {
                                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                        }

                                        {
                                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                        }
                                    }

                                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                                    s.bi_valid += (int)(len - 16);
                                }
                                else
                                {
                                    s.bi_buf |= (ushort)((ushort)(lc) << s.bi_valid);
                                    s.bi_valid += (int)(len);
                                }
                            }
                        }

                        dist--;
                        code = (uint)((dist) < (256) ? _dist_code[dist] : _dist_code[256 + ((dist) >> 7)]);
                        {
                            int len = (int)(dtree[code].dl.len);
                            if ((s.bi_valid) > (16 - len))
                            {
                                int val = (int)(dtree[code].fc.code);
                                s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                                {
                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                    }

                                    {
                                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                    }
                                }

                                s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                                s.bi_valid += (int)(len - 16);
                            }
                            else
                            {
                                s.bi_buf |= (ushort)((dtree[code].fc.code) << s.bi_valid);
                                s.bi_valid += (int)(len);
                            }
                        }

                        extra = (int)(extra_dbits[code]);
                        if (extra != 0)
                        {
                            dist -= ((uint)(base_dist[code]));
                            {
                                int len = (int)(extra);
                                if ((s.bi_valid) > (16 - len))
                                {
                                    int val = (int)(dist);
                                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                                    {
                                        {
                                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                                        }

                                        {
                                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                                        }
                                    }

                                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                                    s.bi_valid += (int)(len - 16);
                                }
                                else
                                {
                                    s.bi_buf |= (ushort)((ushort)(dist) << s.bi_valid);
                                    s.bi_valid += (int)(len);
                                }
                            }
                        }
                    }
                }
                while ((sx) < (s.sym_next));
            {
                int len = (int)(ltree[256].dl.len);
                if ((s.bi_valid) > (16 - len))
                {
                    int val = (int)(ltree[256].fc.code);
                    s.bi_buf |= (ushort)((ushort)(val) << s.bi_valid);
                    {
                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                        }

                        {
                            s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                        }
                    }

                    s.bi_buf = (ushort)((ushort)(val) >> (16 - s.bi_valid));
                    s.bi_valid += (int)(len - 16);
                }
                else
                {
                    s.bi_buf |= (ushort)((ltree[256].fc.code) << s.bi_valid);
                    s.bi_valid += (int)(len);
                }
            }
        }

        public static int detect_data_type(internal_state s)
        {
            uint block_mask = (uint)(0xf3ffc07fUL);
            int n = 0;
            for (n = (int)(0); (n) <= (31); n++, block_mask >>= 1)
            {
                if (((block_mask & 1) != 0) && (s.dyn_ltree[n].fc.freq != 0))
                    return (int)(0);
            }

            if (((s.dyn_ltree[9].fc.freq != 0) || (s.dyn_ltree[10].fc.freq != 0)) || (s.dyn_ltree[13].fc.freq != 0))
                return (int)(1);
            for (n = (int)(32); (n) < (256); n++)
            {
                if (s.dyn_ltree[n].fc.freq != 0)
                    return (int)(1);
            }

            return (int)(0);
        }

        public static uint bi_reverse(uint code, int len)
        {
            uint res = (uint)(0);
            do
            {
                res |= (uint)(code & 1);
                code >>= 1; //Nanook ,
                res <<= 1;
            }
            while ((--len) > (0));
            return (uint)(res >> 1);
        }

        public static void bi_windup(internal_state s)
        {
            if ((s.bi_valid) > (8))
            {
                {
                    {
                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                    }
                }
            }
            else if ((s.bi_valid) > (0))
            {
                {
                    s.pending_buf[s.pending++] = ((byte)(s.bi_buf));
                }
            }

            s.bi_buf = (ushort)(0);
            s.bi_valid = (int)(0);
        }

        public static void bi_flush(internal_state s)
        {
            if ((s.bi_valid) == (16))
            {
                {
                    {
                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) & 0xff));
                    }

                    {
                        s.pending_buf[s.pending++] = ((byte)((s.bi_buf) >> 8));
                    }
                }

                s.bi_buf = (ushort)(0);
                s.bi_valid = (int)(0);
            }
            else if ((s.bi_valid) >= (8))
            {
                {
                    s.pending_buf[s.pending++] = ((byte)(s.bi_buf));
                }

                s.bi_buf >>= 8;
                s.bi_valid -= (int)(8);
            }
        }

        public static int deflateStateCheck(z_stream_s strm)
        {
            internal_state? s;
            if (strm == null) //Nanook not needed || ((strm.zalloc) == null)) || ((strm.zfree) == null))
                return (int)(1);
            s = strm.state;
            if ((((s) == (null)) || (s.strm != strm)) || ((((((((s.status != 42) && (s.status != 57)) && (s.status != 69)) && (s.status != 73)) && (s.status != 91)) && (s.status != 103)) && (s.status != 113)) && (s.status != 666)))
                return (int)(1);
            return (int)(0);
        }

        public static void slide_hash(internal_state s)
        {
            uint n = 0; uint  m  =  0 ;
            ushort* p;
            uint wsize = (uint)(s.w_size);
            n = (uint)(s.hash_size);
            p = &s.head[n];
            do
            {
                m = (uint)(*--p);
                *p = ((ushort)((m) >= (wsize) ? m - wsize : 0));
            }
            while ((--n) != 0);
            n = (uint)(wsize);
            p = &s.prev[n];
            do
            {
                m = (uint)(*--p);
                *p = ((ushort)((m) >= (wsize) ? m - wsize : 0));
            }
            while ((--n) != 0);
        }

        public static void fill_window(internal_state s)
        {
            uint n = 0;
            uint more = 0;
            uint wsize = (uint)(s.w_size);

            do
            {
                more = (uint)(s.window_size - s.lookahead - s.strstart);
                //if ((sizeof(int)) <= (2)) //Nanook not possible in dotnet
                //{
                //    if ((((more) == (0)) && ((s.strstart) == (0))) && ((s.lookahead) == (0)))
                //    {
                //        more = (uint)(wsize);
                //    }
                //    else if ((more) == 0xFFFFFFFF) // ((uint)(-1)))
                //    {
                //        more--;
                //    }
                //}

                if ((s.strstart) >= (wsize + ((s).w_size - (258 + 3 + 1))))
                {
                    CRuntime.memcpy(s.window, s.window + wsize, (ulong)(wsize - more));
                    s.match_start -= (uint)(wsize);
                    s.strstart -= (uint)(wsize);
                    s.block_start -= ((int)(wsize));
                    if ((s.insert) > (s.strstart))
                        s.insert = (uint)(s.strstart);
                    slide_hash(s);
                    more += (uint)(wsize);
                }

                if ((s.strm!.avail_in) == (0))
                    break;
                n = (uint)(read_buf(s.strm, s.window + s.strstart + s.lookahead, (uint)(more)));
                s.lookahead += (uint)(n);
                if ((s.lookahead + s.insert) >= (3))
                {
                    uint str = (uint)(s.strstart - s.insert);
                    s.ins_h = (uint)(s.window[str]);
                    s.ins_h = (uint)(((s.ins_h << (int)s.hash_shift) ^ s.window[str + 1]) & s.hash_mask);
                    while ((s.insert) != 0)
                    {
                        s.ins_h = (uint)(((s.ins_h << (int)s.hash_shift) ^ s.window[str + 3 - 1]) & s.hash_mask);
                        s.prev[str & s.w_mask] = (ushort)(s.head[s.ins_h]);
                        s.head[s.ins_h] = ((ushort)(str));
                        str++;
                        s.insert--;
                        if ((s.lookahead + s.insert) < (3))
                            break;
                    }
                }
            }
            while (((s.lookahead) < (258 + 3 + 1)) && (s.strm.avail_in != 0));
            if ((s.high_water) < (s.window_size))
            {
                uint curr = (uint)(s.strstart + (s.lookahead));
                uint init = 0;
                if ((s.high_water) < (curr))
                {
                    init = (uint)(s.window_size - curr);
                    if ((init) > (258))
                        init = (uint)(258);
                    CRuntime.memset(s.window + curr, (int)(0), (ulong)(init));
                    s.high_water = (uint)(curr + init);
                }
                else if ((s.high_water) < (curr + 258))
                {
                    init = (uint)(curr + 258 - s.high_water);
                    if ((init) > (s.window_size - s.high_water))
                        init = (uint)(s.window_size - s.high_water);
                    CRuntime.memset(s.window + s.high_water, (int)(0), (ulong)(init));
                    s.high_water += (uint)(init);
                }
            }
        }

        public static block_state deflate_stored(internal_state s, int flush)
        {
            uint min_block = (uint)((s.pending_buf_size - 5) > (s.w_size) ? (s.w_size) : (s.pending_buf_size - 5));
            uint len = 0; uint  left  =  0 ;  uint  have  =  0 ;  uint  last  =  ( uint ) ( 0 ) ;
            uint used = (uint)(s.strm!.avail_in);
            do
            {
                len = (uint)(65535);
                have = (uint)((s.bi_valid + 42) >> 3);
                if ((s.strm.avail_out) < (have))
                    break;
                have = (uint)(s.strm.avail_out - have);
                left = (uint)(s.strstart - s.block_start);
                if ((len) > (left + s.strm.avail_in))
                    len = (uint)(left + s.strm.avail_in);
                if ((len) > (have))
                    len = (uint)(have);
                if (((len) < (min_block)) && (((((len) == (0)) && (flush != 4)) || ((flush) == (0))) || (len != left + s.strm.avail_in)))
                    break;
                last = (uint)(((flush) == (4)) && ((len) == (left + s.strm.avail_in)) ? 1 : 0);
                _tr_stored_block(s, null, (uint)(0L), (int)(last));
                s.pending_buf[s.pending - 4] = (byte)(len);
                s.pending_buf[s.pending - 3] = (byte)(len >> 8);
                s.pending_buf[s.pending - 2] = (byte)(~len);
                s.pending_buf[s.pending - 1] = (byte)(~len >> 8);
                flush_pending(s.strm!);
                if ((left) != 0)
                {
                    if ((left) > (len))
                        left = (uint)(len);
                    CRuntime.memcpy(s.strm.next_out, s.window + s.block_start, (ulong)(left));
                    s.strm.next_out += left;
                    s.strm.avail_out -= (uint)(left);
                    s.strm.total_out += (uint)(left);
                    s.block_start += (int)(left);
                    len -= (uint)(left);
                }

                if ((len) != 0)
                {
                    read_buf(s.strm, s.strm.next_out, (uint)(len));
                    s.strm.next_out += len;
                    s.strm.avail_out -= (uint)(len);
                    s.strm.total_out += (uint)(len);
                }
            }
            while ((last) == (0));
            used -= (uint)(s.strm.avail_in);
            if ((used) != 0)
            {
                if ((used) >= (s.w_size))
                {
                    s.matches = (uint)(2);
                    CRuntime.memcpy(s.window, s.strm.next_in - s.w_size, (ulong)(s.w_size));
                    s.strstart = (uint)(s.w_size);
                    s.insert = (uint)(s.strstart);
                }
                else
                {
                    if ((s.window_size - s.strstart) <= (used))
                    {
                        s.strstart -= (uint)(s.w_size);
                        CRuntime.memcpy(s.window, s.window + s.w_size, (ulong)(s.strstart));
                        if ((s.matches) < (2))
                            s.matches++;
                        if ((s.insert) > (s.strstart))
                            s.insert = (uint)(s.strstart);
                    }

                    CRuntime.memcpy(s.window + s.strstart, s.strm.next_in - used, (ulong)(used));
                    s.strstart += (uint)(used);
                    s.insert += (uint)((used) > (s.w_size - s.insert) ? (s.w_size - s.insert) : (used));
                }

                s.block_start = (int)(s.strstart);
            }

            if ((s.high_water) < (s.strstart))
                s.high_water = (uint)(s.strstart);
            if ((last) != 0)
                return (block_state)(block_state.finish_done);
            if ((((flush != 0) && (flush != 4)) && ((s.strm.avail_in) == (0))) && (((int)(s.strstart)) == (s.block_start)))
                return (block_state)(block_state.block_done);
            have = (uint)(s.window_size - s.strstart);
            if (((s.strm.avail_in) > (have)) && ((s.block_start) >= ((int)(s.w_size))))
            {
                s.block_start -= (int)(s.w_size);
                s.strstart -= (uint)(s.w_size);
                CRuntime.memcpy(s.window, s.window + s.w_size, (ulong)(s.strstart));
                if ((s.matches) < (2))
                    s.matches++;
                have += (uint)(s.w_size);
                if ((s.insert) > (s.strstart))
                    s.insert = (uint)(s.strstart);
            }

            if ((have) > (s.strm.avail_in))
                have = (uint)(s.strm.avail_in);
            if ((have) != 0)
            {
                read_buf(s.strm, s.window + s.strstart, (uint)(have));
                s.strstart += (uint)(have);
                s.insert += (uint)((have) > (s.w_size - s.insert) ? (s.w_size - s.insert) : (have));
            }

            if ((s.high_water) < (s.strstart))
                s.high_water = (uint)(s.strstart);
            have = (uint)((s.bi_valid + 42) >> 3);
            have = (uint)((s.pending_buf_size - have) > (65535) ? (65535) : (s.pending_buf_size - have));
            min_block = (uint)((have) > (s.w_size) ? (s.w_size) : (have));
            left = (uint)(s.strstart - s.block_start);
            if (((left) >= (min_block)) || ((((((left) != 0) || ((flush) == (4))) && (flush != 0)) && ((s.strm.avail_in) == (0))) && ((left) <= (have))))
            {
                len = (uint)((left) > (have) ? (have) : (left));
                last = (uint)((((flush) == (4)) && ((s.strm.avail_in) == (0))) && ((len) == (left)) ? 1 : 0);
                _tr_stored_block(s, (sbyte*)(s.window) + s.block_start, (uint)(len), (int)(last));
                s.block_start += (int)(len);
                flush_pending(s.strm!);
            }

            return (block_state)((last) != 0 ? block_state.finish_started : block_state.need_more);
        }

        public static block_state deflate_fast(internal_state s, int flush)
        {
            uint hash_head = 0;
            int bflush = 0;
            for (;;)
            {
                if ((s.lookahead) < (258 + 3 + 1))
                {
                    fill_window(s);
                    if (((s.lookahead) < (258 + 3 + 1)) && ((flush) == (0)))
                    {
                        return (block_state)(block_state.need_more);
                    }

                    if ((s.lookahead) == (0))
                        break;
                }

                hash_head = (uint)(0);
                if ((s.lookahead) >= (3))
                {
                    s.ins_h = (uint)((((s.ins_h) << (int)s.hash_shift) ^ (s.window[(s.strstart) + (3 - 1)])) & s.hash_mask);
                    hash_head = (uint)(s.prev[(s.strstart) & s.w_mask] = (ushort)(s.head[s.ins_h]));
                    s.head[s.ins_h] = ((ushort)(s.strstart));
                }

                if ((hash_head != 0) && ((s.strstart - hash_head) <= ((s).w_size - (258 + 3 + 1))))
                {
                    s.match_length = (uint)(longest_match(s, (uint)(hash_head)));
                }

                if ((s.match_length) >= (3))
                {
                    {
                        byte len = (byte)(s.match_length - 3);
                        ushort dist = (ushort)(s.strstart - s.match_start);
                        s.sym_buf[s.sym_next++] = (byte)(dist);
                        s.sym_buf[s.sym_next++] = (byte)(dist >> 8);
                        s.sym_buf[s.sym_next++] = (byte)(len);
                        dist--;
                        s.dyn_ltree[_length_code[len] + 256 + 1].fc.freq++;
                        s.dyn_dtree[((dist) < (256) ? _dist_code[dist] : _dist_code[256 + ((dist) >> 7)])].fc.freq++;
                        bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                    }

                    s.lookahead -= (uint)(s.match_length);
                    if (((s.match_length) <= (s.max_lazy_match)) && ((s.lookahead) >= (3)))
                    {
                        s.match_length--;
                        do
                        {
                            s.strstart++;
                            s.ins_h = (uint)((((s.ins_h) << (int)s.hash_shift) ^ (s.window[(s.strstart) + (3 - 1)])) & s.hash_mask);
                            hash_head = (uint)(s.prev[(s.strstart) & s.w_mask] = (ushort)(s.head[s.ins_h]));
                            s.head[s.ins_h] = ((ushort)(s.strstart));
                        }
                        while (--s.match_length != 0);
                        s.strstart++;
                    }
                    else
                    {
                        s.strstart += (uint)(s.match_length);
                        s.match_length = (uint)(0);
                        s.ins_h = (uint)(s.window[s.strstart]);
                        s.ins_h = (uint)((((s.ins_h) << (int)s.hash_shift) ^ (s.window[s.strstart + 1])) & s.hash_mask);
                    }
                }
                else
                {
                    {
                        byte cc = (byte)(s.window[s.strstart]);
                        s.sym_buf[s.sym_next++] = (byte)(0);
                        s.sym_buf[s.sym_next++] = (byte)(0);
                        s.sym_buf[s.sym_next++] = (byte)(cc);
                        s.dyn_ltree[cc].fc.freq++;
                        bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                    }

                    s.lookahead--;
                    s.strstart++;
                }

                if ((bflush) != 0)
                {
                    {
                        _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                        s.block_start = (int)(s.strstart);
                        flush_pending(s.strm!);
                    }

                    if ((s.strm!.avail_out) == (0))
                        return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
                }
            }

            s.insert = (uint)((s.strstart) < (3 - 1) ? s.strstart : 3 - 1);
            if ((flush) == (4))
            {
                {
                    {
                        _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(1));
                        s.block_start = (int)(s.strstart);
                        flush_pending(s.strm!);
                    }

                    if ((s.strm!.avail_out) == (0))
                        return (block_state)((1) != 0 ? block_state.finish_started : block_state.need_more);
                }

                return (block_state)(block_state.finish_done);
            }

            if ((s.sym_next) != 0)
            {
                {
                    _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                    s.block_start = (int)(s.strstart);
                    flush_pending(s.strm!);
                }

                if ((s.strm!.avail_out) == (0))
                    return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
            }

            return (block_state)(block_state.block_done);
        }

        public static block_state deflate_slow(internal_state s, int flush)
        {
            uint hash_head = 0;
            int bflush = 0;
            for (;;)
            {
                if ((s.lookahead) < (258 + 3 + 1))
                {
                    fill_window(s);
                    if (((s.lookahead) < (258 + 3 + 1)) && ((flush) == (0)))
                    {
                        return (block_state)(block_state.need_more);
                    }

                    if ((s.lookahead) == (0))
                        break;
                }

                hash_head = (uint)(0);
                if ((s.lookahead) >= (3))
                {
                    s.ins_h = (uint)((((s.ins_h) << (int)s.hash_shift) ^ (s.window[(s.strstart) + (3 - 1)])) & s.hash_mask);
                    hash_head = (uint)(s.prev[(s.strstart) & s.w_mask] = (ushort)(s.head[s.ins_h]));
                    s.head[s.ins_h] = ((ushort)(s.strstart));
                }

                s.prev_length = (uint)(s.match_length); //Nanook ,
                s.prev_match = (uint)(s.match_start);
                s.match_length = (uint)(3 - 1);
                if (((hash_head != 0) && ((s.prev_length) < (s.max_lazy_match))) && ((s.strstart - hash_head) <= ((s).w_size - (258 + 3 + 1))))
                {
                    s.match_length = (uint)(longest_match(s, (uint)(hash_head)));
                    if (((s.match_length) <= (5)) && (((s.strategy) == (1)) || (((s.match_length) == (3)) && ((s.strstart - s.match_start) > (4096)))))
                    {
                        s.match_length = (uint)(3 - 1);
                    }
                }

                if (((s.prev_length) >= (3)) && ((s.match_length) <= (s.prev_length)))
                {
                    uint max_insert = (uint)(s.strstart + s.lookahead - 3);
                    {
                        byte len = (byte)(s.prev_length - 3);
                        ushort dist = (ushort)(s.strstart - 1 - s.prev_match);
                        s.sym_buf[s.sym_next++] = (byte)(dist);
                        s.sym_buf[s.sym_next++] = (byte)(dist >> 8);
                        s.sym_buf[s.sym_next++] = (byte)(len);
                        dist--;
                        s.dyn_ltree[_length_code[len] + 256 + 1].fc.freq++;
                        s.dyn_dtree[((dist) < (256) ? _dist_code[dist] : _dist_code[256 + ((dist) >> 7)])].fc.freq++;
                        bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                    }

                    s.lookahead -= (uint)(s.prev_length - 1);
                    s.prev_length -= (uint)(2);
                    do
                    {
                        if ((++s.strstart) <= (max_insert))
                        {
                            s.ins_h = (uint)((((s.ins_h) << (int)s.hash_shift) ^ (s.window[(s.strstart) + (3 - 1)])) & s.hash_mask);
                            hash_head = (uint)(s.prev[(s.strstart) & s.w_mask] = (ushort)(s.head[s.ins_h]));
                            s.head[s.ins_h] = ((ushort)(s.strstart));
                        }
                    }
                    while (--s.prev_length != 0);
                    s.match_available = (int)(0);
                    s.match_length = (uint)(3 - 1);
                    s.strstart++;
                    if ((bflush) != 0)
                    {
                        {
                            _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                            s.block_start = (int)(s.strstart);
                            flush_pending(s.strm!);
                        }

                        if ((s.strm!.avail_out) == (0))
                            return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
                    }
                }
                else if ((s.match_available) != 0)
                {
                    {
                        byte cc = (byte)(s.window[s.strstart - 1]);
                        s.sym_buf[s.sym_next++] = (byte)(0);
                        s.sym_buf[s.sym_next++] = (byte)(0);
                        s.sym_buf[s.sym_next++] = (byte)(cc);
                        s.dyn_ltree[cc].fc.freq++;
                        bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                    }

                    if ((bflush) != 0)
                    {
                        {
                            _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                            s.block_start = (int)(s.strstart);
                            flush_pending(s.strm!);
                        }
                    }

                    s.strstart++;
                    s.lookahead--;
                    if ((s.strm!.avail_out) == (0))
                        return (block_state)(block_state.need_more);
                }
                else
                {
                    s.match_available = (int)(1);
                    s.strstart++;
                    s.lookahead--;
                }
            }

            if ((s.match_available) != 0)
            {
                {
                    byte cc = (byte)(s.window[s.strstart - 1]);
                    s.sym_buf[s.sym_next++] = (byte)(0);
                    s.sym_buf[s.sym_next++] = (byte)(0);
                    s.sym_buf[s.sym_next++] = (byte)(cc);
                    s.dyn_ltree[cc].fc.freq++;
                    bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                }

                s.match_available = (int)(0);
            }

            s.insert = (uint)((s.strstart) < (3 - 1) ? s.strstart : 3 - 1);
            if ((flush) == (4))
            {
                {
                    {
                        _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(1));
                        s.block_start = (int)(s.strstart);
                        flush_pending(s.strm!);
                    }

                    if ((s.strm!.avail_out) == (0))
                        return (block_state)((1) != 0 ? block_state.finish_started : block_state.need_more);
                }

                return (block_state)(block_state.finish_done);
            }

            if ((s.sym_next) != 0)
            {
                {
                    _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                    s.block_start = (int)(s.strstart);
                    flush_pending(s.strm!);
                }

                if ((s.strm!.avail_out) == (0))
                    return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
            }

            return (block_state)(block_state.block_done);
        }

        public static block_state deflate_rle(internal_state s, int flush)
        {
            int bflush = 0;
            uint prev = 0;
            byte* scan; byte  * strend ;
            for (;;)
            {
                if ((s.lookahead) <= (258))
                {
                    fill_window(s);
                    if (((s.lookahead) <= (258)) && ((flush) == (0)))
                    {
                        return (block_state)(block_state.need_more);
                    }

                    if ((s.lookahead) == (0))
                        break;
                }

                s.match_length = (uint)(0);
                if (((s.lookahead) >= (3)) && ((s.strstart) > (0)))
                {
                    scan = s.window + s.strstart - 1;
                    prev = (uint)(*scan);
                    if ((((prev) == (*++scan)) && ((prev) == (*++scan))) && ((prev) == (*++scan)))
                    {
                        strend = s.window + s.strstart + 258;
                        do
                        {
                        }
                        while ((((((((((prev) == (*++scan)) && ((prev) == (*++scan))) && ((prev) == (*++scan))) && ((prev) == (*++scan))) && ((prev) == (*++scan))) && ((prev) == (*++scan))) && ((prev) == (*++scan))) && ((prev) == (*++scan))) && ((scan) < (strend)));
                        s.match_length = (uint)(258 - (uint)(strend - scan));
                        if ((s.match_length) > (s.lookahead))
                            s.match_length = (uint)(s.lookahead);
                    }
                }

                if ((s.match_length) >= (3))
                {
                    {
                        byte len = (byte)(s.match_length - 3);
                        ushort dist = (ushort)(1);
                        s.sym_buf[s.sym_next++] = (byte)(dist);
                        s.sym_buf[s.sym_next++] = (byte)(dist >> 8);
                        s.sym_buf[s.sym_next++] = (byte)(len);
                        dist--;
                        s.dyn_ltree[_length_code[len] + 256 + 1].fc.freq++;
                        s.dyn_dtree[((dist) < (256) ? _dist_code[dist] : _dist_code[256 + ((dist) >> 7)])].fc.freq++;
                        bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                    }

                    s.lookahead -= (uint)(s.match_length);
                    s.strstart += (uint)(s.match_length);
                    s.match_length = (uint)(0);
                }
                else
                {
                    {
                        byte cc = (byte)(s.window[s.strstart]);
                        s.sym_buf[s.sym_next++] = (byte)(0);
                        s.sym_buf[s.sym_next++] = (byte)(0);
                        s.sym_buf[s.sym_next++] = (byte)(cc);
                        s.dyn_ltree[cc].fc.freq++;
                        bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                    }

                    s.lookahead--;
                    s.strstart++;
                }

                if ((bflush) != 0)
                {
                    {
                        _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                        s.block_start = (int)(s.strstart);
                        flush_pending(s.strm!);
                    }

                    if ((s.strm!.avail_out) == (0))
                        return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
                }
            }

            s.insert = (uint)(0);
            if ((flush) == (4))
            {
                {
                    {
                        _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(1));
                        s.block_start = (int)(s.strstart);
                        flush_pending(s.strm!);
                    }

                    if ((s.strm!.avail_out) == (0))
                        return (block_state)((1) != 0 ? block_state.finish_started : block_state.need_more);
                }

                return (block_state)(block_state.finish_done);
            }

            if ((s.sym_next) != 0)
            {
                {
                    _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                    s.block_start = (int)(s.strstart);
                    flush_pending(s.strm!);
                }

                if ((s.strm!.avail_out) == (0))
                    return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
            }

            return (block_state)(block_state.block_done);
        }

        public static block_state deflate_huff(internal_state s, int flush)
        {
            int bflush = 0;
            for (;;)
            {
                if ((s.lookahead) == (0))
                {
                    fill_window(s);
                    if ((s.lookahead) == (0))
                    {
                        if ((flush) == (0))
                            return (block_state)(block_state.need_more);
                        break;
                    }
                }

                s.match_length = (uint)(0);
                {
                    byte cc = (byte)(s.window[s.strstart]);
                    s.sym_buf[s.sym_next++] = (byte)(0);
                    s.sym_buf[s.sym_next++] = (byte)(0);
                    s.sym_buf[s.sym_next++] = (byte)(cc);
                    s.dyn_ltree[cc].fc.freq++;
                    bflush = (int)((s.sym_next) == (s.sym_end) ? 1 : 0);
                }

                s.lookahead--;
                s.strstart++;
                if ((bflush) != 0)
                {
                    {
                        _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                        s.block_start = (int)(s.strstart);
                        flush_pending(s.strm!);
                    }

                    if ((s.strm!.avail_out) == (0))
                        return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
                }
            }

            s.insert = (uint)(0);
            if ((flush) == (4))
            {
                {
                    {
                        _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(1));
                        s.block_start = (int)(s.strstart);
                        flush_pending(s.strm!);
                    }

                    if ((s.strm!.avail_out) == (0))
                        return (block_state)((1) != 0 ? block_state.finish_started : block_state.need_more);
                }

                return (block_state)(block_state.finish_done);
            }

            if ((s.sym_next) != 0)
            {
                {
                    _tr_flush_block(s, ((s.block_start) >= (0L) ? (sbyte*)(&s.window[(uint)(s.block_start)]) : (sbyte*)0), (uint)((int)(s.strstart) - s.block_start), (int)(0));
                    s.block_start = (int)(s.strstart);
                    flush_pending(s.strm!);
                }

                if ((s.strm!.avail_out) == (0))
                    return (block_state)((0) != 0 ? block_state.finish_started : block_state.need_more);
            }

            return (block_state)(block_state.block_done);
        }

        public static void lm_init(internal_state s)
        {
            s.window_size = (uint)((uint)(2L) * s.w_size);
            do
            {
                s.head[s.hash_size - 1] = (ushort)(0);
                CRuntime.memset((byte*)(s.head), (int)(0), (ulong)((s.hash_size - 1) * sizeof(ushort)));
            }
            while ((0) != 0);
            s.max_lazy_match = (uint)(configuration_table[s.level].max_lazy);
            s.good_match = (uint)(configuration_table[s.level].good_length);
            s.nice_match = (int)(configuration_table[s.level].nice_length);
            s.max_chain_length = (uint)(configuration_table[s.level].max_chain);
            s.strstart = (uint)(0);
            s.block_start = (int)(0L);
            s.lookahead = (uint)(0);
            s.insert = (uint)(0);
            s.match_length = (uint)(s.prev_length = (uint)(3 - 1));
            s.match_available = (int)(0);
            s.ins_h = (uint)(0);
        }

        public static void putShortMSB(internal_state s, uint b)
        {
            {
                s.pending_buf[s.pending++] = ((byte)(b >> 8));
            }

            {
                s.pending_buf[s.pending++] = ((byte)(b & 0xff));
            }
        }

        public static void flush_pending(z_stream_s strm)
        {
            uint len = 0;
            internal_state? s = strm.state;
            _tr_flush_bits(s!);
            len = (uint)(s!.pending);
            if ((len) > (strm.avail_out))
                len = (uint)(strm.avail_out);
            if ((len) == (0))
                return;
            CRuntime.memcpy(strm.next_out, s.pending_out, (ulong)(len));
            strm.next_out += len;
            s.pending_out += len;
            strm.total_out += (uint)(len);
            strm.avail_out -= (uint)(len);
            s.pending -= (uint)(len);
            if ((s.pending) == (0))
            {
                s.pending_out = s.pending_buf;
            }
        }

        public static uint read_buf(z_stream_s strm, byte* buf, uint size)
        {
            uint len = (uint)(strm.avail_in);
            if ((len) > (size))
                len = (uint)(size);
            if ((len) == (0))
                return (uint)(0);
            strm.avail_in -= (uint)(len);
            CRuntime.memcpy(buf, strm.next_in, (ulong)(len));
            if ((strm.state!.wrap) == (1))
            {
                strm.adler = (uint)(adler32((uint)(strm.adler), buf, (uint)(len)));
            }
            else if ((strm.state.wrap) == (2))
            {
                strm.adler = (uint)(crc32((uint)(strm.adler), buf, (uint)(len)));
            }

            strm.next_in += len;
            strm.total_in += (uint)(len);
            return (uint)(len);
        }

        public static uint longest_match(internal_state s, uint cur_match)
        {
            uint chain_length = (uint)(s.max_chain_length);
            byte* scan = s.window + s.strstart;
            byte* match;
            int len = 0;
            int best_len = (int)(s.prev_length);
            int nice_match = (int)(s.nice_match);
            uint limit = (uint)((s.strstart) > ((s).w_size - (258 + 3 + 1)) ? s.strstart - ((s).w_size - (258 + 3 + 1)) : 0);
            ushort* prev = s.prev;
            uint wmask = (uint)(s.w_mask);
            byte* strend = s.window + s.strstart + 258;
            byte scan_end1 = (byte)(scan[best_len - 1]);
            byte scan_end = (byte)(scan[best_len]);
            if ((s.prev_length) >= (s.good_match))
            {
                chain_length >>= 2;
            }

            if (((uint)(nice_match)) > (s.lookahead))
                nice_match = ((int)(s.lookahead));
            do
            {
                match = s.window + cur_match;
                if ((((match[best_len] != scan_end) || (match[best_len - 1] != scan_end1)) || (*match != *scan)) || (*++match != scan[1]))
                    continue;
                scan += 2; //Nanook ,
                match++;
                do
                {
                }
                while ((((((((((*++scan) == (*++match)) && ((*++scan) == (*++match))) && ((*++scan) == (*++match))) && ((*++scan) == (*++match))) && ((*++scan) == (*++match))) && ((*++scan) == (*++match))) && ((*++scan) == (*++match))) && ((*++scan) == (*++match))) && ((scan) < (strend)));
                len = (int)(258 - (int)(strend - scan));
                scan = strend - 258;
                if ((len) > (best_len))
                {
                    s.match_start = (uint)(cur_match);
                    best_len = (int)(len);
                    if ((len) >= (nice_match))
                        break;
                    scan_end1 = (byte)(scan[best_len - 1]);
                    scan_end = (byte)(scan[best_len]);
                }
            }
            while (((cur_match = (uint)(prev[cur_match & wmask])) > (limit)) && (--chain_length != 0));
            if (((uint)(best_len)) <= (s.lookahead))
                return (uint)(best_len);
            return (uint)(s.lookahead);
        }

        public static int inflate_table(codetype type, ushort* lens, uint codes, code[] codeArray, ref int table, ref uint bits, ushort[] work)
        {
            uint len = 0;
            uint sym = 0;
            uint min = 0; uint max = 0;
            uint root = 0;
            uint curr = 0;
            uint drop = 0;
            int left = 0;
            uint used = 0;
            uint huff = 0;
            uint incr = 0;
            uint fill = 0;
            uint low = 0;
            uint mask = 0;
            code here = new code();
            int next;
            ushort[] _base_;
            ushort[] extra;
            uint match = 0;
            ushort* count = stackalloc ushort[16];
            ushort* offs = stackalloc ushort[16];
            for (len = (uint)(0); (len) <= (15); len++)
            {
                count[len] = (ushort)(0);
            }

            for (sym = (uint)(0); (sym) < (codes); sym++)
            {
                count[lens[sym]]++;
            }

            root = (uint)(bits);
            for (max = (uint)(15); (max) >= (1); max--)
            {
                if (count[max] != 0)
                    break;
            }

            if ((root) > (max))
                root = (uint)(max);
            if ((max) == (0))
            {
                here.op = ((byte)(64));
                here.bits = ((byte)(1));
                here.val = ((ushort)(0));
                codeArray[table++] = (code)(here);
                codeArray[table++] = (code)(here);
                bits = (uint)(1);
                return (int)(0);
            }

            for (min = (uint)(1); (min) < (max); min++)
            {
                if (count[min] != 0)
                    break;
            }

            if ((root) < (min))
                root = (uint)(min);
            left = (int)(1);
            for (len = (uint)(1); (len) <= (15); len++)
            {
                left <<= 1;
                left -= (int)(count[len]);
                if ((left) < (0))
                    return (int)(-1);
            }

            if (((left) > (0)) && (((type) == (codetype.CODES)) || (max != 1)))
                return (int)(-1);
            offs[1] = (ushort)(0);
            for (len = (uint)(1); (len) < (15); len++)
            {
                offs[len + 1] = (ushort)(offs[len] + count[len]);
            }

            for (sym = (uint)(0); (sym) < (codes); sym++)
            {
                if (lens[sym] != 0)
                    work[offs[lens[sym]]++] = ((ushort)(sym));
            }

            switch (type)
            {
                case codetype.CODES:
                    _base_ = extra = work;
                    match = (uint)(20);
                    break;
                case codetype.LENS:
                    _base_ = inflate_table_lbase;
                    extra = inflate_table_lext;
                    match = (uint)(257);
                    break;
                default:
                    _base_ = inflate_table_dbase;
                    extra = inflate_table_dext;
                    match = (uint)(0);
                    break;
            }

            huff = (uint)(0);
            sym = (uint)(0);
            len = (uint)(min);
            next = table;
            curr = (uint)(root);
            drop = (uint)(0);
            low = ((uint)(0xFFFFFFFF)); //Nanook was -1
            used = (uint)(1U << (int)root); //Nanook added (int)
            mask = (uint)(used - 1);
            if ((((type) == (codetype.LENS)) && ((used) > (852))) || (((type) == (codetype.DISTS)) && ((used) > (592))))
                return (int)(1);
            for (; ; )
            {
                here.bits = ((byte)(len - drop));
                if ((work[sym] + 1U) < (match))
                {
                    here.op = ((byte)(0));
                    here.val = (ushort)(work[sym]);
                }
                else if ((work[sym]) >= (match))
                {
                    here.op = ((byte)(extra[work[sym] - match]));
                    here.val = (ushort)(_base_[work[sym] - match]);
                }
                else
                {
                    here.op = ((byte)(32 + 64));
                    here.val = (ushort)(0);
                }

                incr = (uint)(1U << (int)(len - drop)); //Nanook added (int)
                fill = (uint)(1U << (int)curr); //Nanook added (int)
                min = (uint)(fill);
                do
                {
                    fill -= (uint)(incr);
                    codeArray[next + (huff >> (int)drop) + fill] = (code)(here); //Nanook added (int)
                }
                while (fill != 0);
                incr = (uint)(1U << (int)(len - 1)); //Nanook added (int)
                while ((huff & incr) != 0)
                {
                    incr >>= 1;
                }

                if (incr != 0)
                {
                    huff &= (uint)(incr - 1);
                    huff += (uint)(incr);
                }
                else
                    huff = (uint)(0);
                sym++;
                if ((--(count[len])) == (0))
                {
                    if ((len) == (max))
                        break;
                    len = (uint)(lens[work[sym]]);
                }

                if (((len) > (root)) && ((huff & mask) != low))
                {
                    if ((drop) == (0))
                        drop = (uint)(root);
                    next += (int)min;
                    curr = (uint)(len - drop);
                    left = (int)(1 << (int)curr); //Nanook added (int)
                    while ((curr + drop) < (max))
                    {
                        left -= (int)(count[curr + drop]);
                        if ((left) <= (0))
                            break;
                        curr++;
                        left <<= 1;
                    }

                    used += (uint)(1U << (int)curr); //Nanook added (int)
                    if ((((type) == (codetype.LENS)) && ((used) > (852))) || (((type) == (codetype.DISTS)) && ((used) > (592))))
                        return (int)(1);
                    low = (uint)(huff & mask);
                    codeArray[table + low].op = ((byte)(curr));
                    codeArray[table + low].bits = ((byte)(root));
                    codeArray[table + low].val = ((ushort)(next - table));
                }
            }

            if (huff != 0)
            {
                here.op = ((byte)(64));
                here.bits = ((byte)(len - drop));
                here.val = ((ushort)(0));
                codeArray[next + huff] = (code)(here);
            }

            table += (int)used;
            bits = (uint)(root);
            return (int)(0);
        }

        public static void inflate_fast(z_stream_s strm, uint start)
        {
            inflate_state? state;
            byte* _in_;
            byte* last;
            byte* _out_;
            byte* beg;
            byte* end;
            uint wsize = 0;
            uint whave = 0;
            uint wnext = 0;
            byte* window;
            uint hold = 0;
            uint bits = 0;
            int lcode;
            int dcode;
            uint lmask = 0;
            uint dmask = 0;
            code here;
            uint op = 0;
            uint len = 0;
            uint dist = 0;
            byte* from;
            state = (inflate_state)(strm.i_state!);
            _in_ = strm.next_in;
            last = _in_ + (strm.avail_in - 5);
            _out_ = strm.next_out;
            beg = _out_ - (start - strm.avail_out);
            end = _out_ + (strm.avail_out - 257);
            wsize = (uint)(state.wsize);
            whave = (uint)(state.whave);
            wnext = (uint)(state.wnext);
            window = state.window;
            hold = (uint)(state.hold);
            bits = (uint)(state.bits);
            lcode = state.lencode;
            dcode = state.distcode;
            lmask = (uint)((1U << (int)state.lenbits) - 1); //Nanook added (int)
            dmask = (uint)((1U << (int)state.distbits) - 1); //Nanook added (int)
            do
            {
                if ((bits) < (15))
                {
                    hold += (uint)((uint)(*_in_++) << (int)bits); //Nanook added (int)
                    bits += (uint)(8);
                    hold += (uint)((uint)(*_in_++) << (int)bits); //Nanook added (int)
                    bits += (uint)(8);
                }

                here = state.lencodes![(int)(lcode + (hold & lmask))];
            dolen:
                ;
                op = ((uint)(here.bits));
                hold >>= (int)op; //Nanook added (int)
                bits -= (uint)(op);
                op = ((uint)(here.op));
                if ((op) == (0))
                {
                    *_out_++ = ((byte)(here.val));
                }
                else if ((op & 16) != 0)
                {
                    len = ((uint)(here.val));
                    op &= (uint)(15);
                    if ((op) != 0)
                    {
                        if ((bits) < (op))
                        {
                            hold += (uint)((uint)(*_in_++) << (int)bits); //Nanook added (int)
                            bits += (uint)(8);
                        }

                        len += (uint)(hold & ((1U << (int)op) - 1)); //Nanook added (int)
                        hold >>= (int)op; //Nanook added (int)
                        bits -= (uint)(op);
                    }

                    if ((bits) < (15))
                    {
                        hold += (uint)((uint)(*_in_++) << (int)bits); //Nanook added (int)
                        bits += (uint)(8);
                        hold += (uint)((uint)(*_in_++) << (int)bits); //Nanook added (int)
                        bits += (uint)(8);
                    }

                    here = state.distcodes![(int)(dcode + (hold & dmask))];
                dodist:
                    ;
                    op = ((uint)(here.bits));
                    hold >>= (int)op; //Nanook added (int)
                    bits -= (uint)(op);
                    op = ((uint)(here.op));
                    if ((op & 16) != 0)
                    {
                        dist = ((uint)(here.val));
                        op &= (uint)(15);
                        if ((bits) < (op))
                        {
                            hold += (uint)((uint)(*_in_++) << (int)bits); //Nanook added (int)
                            bits += (uint)(8);
                            if ((bits) < (op))
                            {
                                hold += (uint)((uint)(*_in_++) << (int)bits); //Nanook added (int)
                                bits += (uint)(8);
                            }
                        }

                        dist += (uint)(hold & ((1U << (int)op) - 1)); //Nanook added (int)
                        hold >>= (int)op; //Nanook added (int)
                        bits -= (uint)(op);
                        op = ((uint)(_out_ - beg));
                        if ((dist) > (op))
                        {
                            op = (uint)(dist - op);
                            if ((op) > (whave))
                            {
                                if ((state.sane) != 0)
                                {
                                    strm.msg = "invalid distance too far back";
                                    state.mode = (inflate_mode.BAD);
                                    break;
                                }
                            }

                            from = window;
                            if ((wnext) == (0))
                            {
                                from += wsize - op;
                                if ((op) < (len))
                                {
                                    len -= (uint)(op);
                                    do
                                    {
                                        *_out_++ = (byte)(*from++);
                                    }
                                    while ((--op) != 0);
                                    from = _out_ - dist;
                                }
                            }
                            else if ((wnext) < (op))
                            {
                                from += wsize + wnext - op;
                                op -= (uint)(wnext);
                                if ((op) < (len))
                                {
                                    len -= (uint)(op);
                                    do
                                    {
                                        *_out_++ = (byte)(*from++);
                                    }
                                    while ((--op) != 0);
                                    from = window;
                                    if ((wnext) < (len))
                                    {
                                        op = (uint)(wnext);
                                        len -= (uint)(op);
                                        do
                                        {
                                            *_out_++ = (byte)(*from++);
                                        }
                                        while ((--op) != 0);
                                        from = _out_ - dist;
                                    }
                                }
                            }
                            else
                            {
                                from += wnext - op;
                                if ((op) < (len))
                                {
                                    len -= (uint)(op);
                                    do
                                    {
                                        *_out_++ = (byte)(*from++);
                                    }
                                    while ((--op) != 0);
                                    from = _out_ - dist;
                                }
                            }

                            while ((len) > (2))
                            {
                                *_out_++ = (byte)(*from++);
                                *_out_++ = (byte)(*from++);
                                *_out_++ = (byte)(*from++);
                                len -= (uint)(3);
                            }

                            if ((len) != 0)
                            {
                                *_out_++ = (byte)(*from++);
                                if ((len) > (1))
                                    *_out_++ = (byte)(*from++);
                            }
                        }
                        else
                        {
                            from = _out_ - dist;
                            do
                            {
                                *_out_++ = (byte)(*from++);
                                *_out_++ = (byte)(*from++);
                                *_out_++ = (byte)(*from++);
                                len -= (uint)(3);
                            }
                            while ((len) > (2));
                            if ((len) != 0)
                            {
                                *_out_++ = (byte)(*from++);
                                if ((len) > (1))
                                    *_out_++ = (byte)(*from++);
                            }
                        }
                    }
                    else if ((op & 64) == (0))
                    {
                        here = state.distcodes[(int)(dcode + here.val + (hold & ((1U << (int)op) - 1)))]; //Nanook added (int)
                        goto dodist;
                    }
                    else
                    {
                        strm.msg = "invalid distance code";
                        state.mode = (inflate_mode.BAD);
                        break;
                    }
                }
                else if ((op & 64) == (0))
                {
                    here = state.lencodes[(int)(lcode + here.val + (hold & ((1U << (int)op) - 1)))]; //Nanook added (int)
                    goto dolen;
                }
                else if ((op & 32) != 0)
                {
                    state.mode = (inflate_mode.TYPE);
                    break;
                }
                else
                {
                    strm.msg = "invalid literal/length code";
                    state.mode = (inflate_mode.BAD);
                    break;
                }
            }
            while (((_in_) < (last)) && ((_out_) < (end)));
            len = (uint)(bits >> 3);
            _in_ -= len;
            bits -= (uint)(len << 3);
            hold &= (uint)((1U << (int)bits) - 1); //Nanook added (int)
            strm.next_in = _in_;
            strm.next_out = _out_;
            strm.avail_in = ((uint)((_in_) < (last) ? 5 + (last - _in_) : 5 - (_in_ - last)));
            strm.avail_out = ((uint)((_out_) < (end) ? 257 + (end - _out_) : 257 - (_out_ - end)));
            state.hold = (uint)(hold);
            state.bits = (uint)(bits);
            return;
        }

        public static int inflateStateCheck(z_stream_s strm)
        {
            inflate_state? state;
            if ((strm) == (null)) //Nanook || ((strm.zalloc) == null)) || ((strm.zfree) == null)) //Nanook
                return (int)(1);
            state = (inflate_state)(strm.i_state!);
            if (((((state) == (null)) || (state.strm != strm)) || ((state.mode) < (inflate_mode.HEAD))) || ((state.mode) > (inflate_mode.SYNC)))
                return (int)(1);
            return (int)(0);
        }

        public static void fixedtables(inflate_state state)
        {
            state.lencodes = lenfix;
            state.lencode = 0; //Nanook index not pointers lenfix;
            state.lenbits = (uint)(9);
            state.distcodes = distfix;
            state.distcode = 0; //Nanook index not pointers distfix;
            state.distbits = (uint)(5);
        }

        public static int updatewindow(z_stream_s strm, byte* end, uint copy)
        {
            inflate_state? state;
            uint dist = 0;
            state = (inflate_state)(strm.i_state!);
            if ((state.window) == (null))
            {
                //state.window = (byte*)(*((strm).zalloc)((strm).opaque, (uint)(1U << (int)state.wbits), (uint)(sizeof(unsignedchar)))); //Nanook added (int)
                state.windowArray = new UnsafeArray1D<byte>(1 << (int)state.wbits);
                state.window = (byte*)state.windowArray.ToPointer();
                if ((state.window) == (null))
                    return (int)(1);
            }

            if ((state.wsize) == (0))
            {
                state.wsize = (uint)(1U << (int)state.wbits); //Nanook added (int)
                state.wnext = (uint)(0);
                state.whave = (uint)(0);
            }

            if ((copy) >= (state.wsize))
            {
                CRuntime.memcpy(state.window, end - state.wsize, (ulong)(state.wsize));
                state.wnext = (uint)(0);
                state.whave = (uint)(state.wsize);
            }
            else
            {
                dist = (uint)(state.wsize - state.wnext);
                if ((dist) > (copy))
                    dist = (uint)(copy);
                CRuntime.memcpy(state.window + state.wnext, end - copy, (ulong)(dist));
                copy -= (uint)(dist);
                if ((copy) != 0)
                {
                    CRuntime.memcpy(state.window, end - copy, (ulong)(copy));
                    state.wnext = (uint)(copy);
                    state.whave = (uint)(state.wsize);
                }
                else
                {
                    state.wnext += (uint)(dist);
                    if ((state.wnext) == (state.wsize))
                        state.wnext = (uint)(0);
                    if ((state.whave) < (state.wsize))
                        state.whave += (uint)(dist);
                }
            }

            return (int)(0);
        }

        public static uint syncsearch(uint* have, byte* buf, uint len)
        {
            uint got = 0;
            uint next = 0;
            got = (uint)(*have);
            next = (uint)(0);
            while (((next) < (len)) && ((got) < (4)))
            {
                if (((int)(buf[next])) == ((got) < (2) ? 0 : 0xff))
                    got++;
                else if ((buf[next]) != 0)
                    got = (uint)(0);
                else
                    got = (uint)(4 - got);
                next++;
            }

            *have = (uint)(got);
            return (uint)(next);
        }

    }
}
