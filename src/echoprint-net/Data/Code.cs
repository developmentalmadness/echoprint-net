using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echoprint_net.Data
{
    /*
{
"metadata":{
   "artist":"Various Artists", 
*      "release":"Music For Meditation", 
*      "title":"Angels Flight", 
*      "genre":"Jazz", 
*      "bitrate":64,
*      "sample_rate":22050, 
*      "duration":243, 
*      "filename":"E:\\Music\\Various Artists\\Yoga Zone - Music For Meditation\\12 Angels Flight.mp3", 
*      "samples_decoded":330902, 
*      "given_duration":30, 
*      "start_offset":10, 
*      "version":4.12, 
*      "codegen_time":0.159000, 
*      "decode_time":0.228000
*   }, 
*   "code_count":756, 
*   "code":"eJy1mW2OZCkORbeEMWCzHMCw_yXMIUaa7C4pyB-lUbduV73KeAH4flGdksRJ3yFLeUEeL2jrBWO8YLcHqNoLen_Bmg8odTygSn-BjhfcTX-Hu7TvsNYDOJAHyN3WV8j3R76D1Bfc938Hqy8Y8wWrvuD0B6jrC9Z4wYdc36DIfsH2B_zV9Gd7gER5wdkP-GW-2V9Q5gM06QvKekHbL-j-gJKfEOsB9S7tO-TyghYv8PWAdq3lO_T0gLd7_zb9t7c_p_-LQucDfpm-tweUGyzfweQF8wlvbth5wdwvOPUBEvsFfzNBTy_4m4Rt5QWeH1Byf4H5C3p-wbYH_KLQbg94e-xvLvruSOUF4wVa4gVvF3V_wRXxd9gveDeoX2b0dmDtL3gn7KXedxhPmPMB_0f9XkF8B58vGPqCv9H-272fzCmpv-Dt7W_m_NK9ywue7v3L_egW5e_wdoaaXvCe4GoPUF8vmPqAchf-Hd7-vM8DqsYL3jN6NKigcbYSUuvoYWXF0HFUztzteJFz6tr8N2uUqS5r-KCHaEPdo46zA2LXEackG06dyzKkt1mzhHDDSlqbnX7uV-yWQ0RWLDm6qAaciS9Znn0dcVY6g8ouaWAr4_S1zxzowqJnj6m5W-_1xMi5RPNurnm0kck-WWn02r2uM91tqp3WaplcpmbvjT26Lb5-e-ufe-ws1fWG1_Djp0rkPlhFK0c65lKWDh5wOG6jqY1kUixLrpmLkOr0lqvPSGZttqVTsukZq5rGlJb8rLH4xKktrzK7Tsvz2OYbzNJJo_HCUWOfkOOL9XW37bJ3OUd4v02OavYdy4eIl2qcu7Tqmz_Mu6wfKHelfzz7Ac8NQbD6whQXk-nlWOXUh5WIXVrD8GL1wcVjmlZt1rKN48Ns1xW1tCyc9JZgzR68xLz5dq9u_SOZmJvqknPTcoaYcgkVIfmDoXKYO9XN_sZZiM933QwKXlHBm1fGcKbYLLvbHr2vZUilhg4Ofuw-p_d7os3i1FkKXLDwvkdm8LIqZ75aK815Vmu005gm62h5MPDeQFtYoPtartalw_Aovgey6jDE4LYPNgmzWpzMYPuqbbMAXX3P1CTPomMrB-jmc-ryfZjHds6pyT6tnlQK52Cd1RDjddUrK5S0rdYDUQNqbq1StXJsxSVLr1EtGA7Plsk6tRfop96FTj1_oMlQ-ePZD5ToKCyd4Lx8RJK1y0Z_sKrV-9uknIF3ZjBsByd-KWi9pM0dKMuR0Y8ZjPZqK6wVmcgM6p17V0FqFc7Mk4slxGPGcoWoGxNaBcOEkRsDSJvSFR_WRzSOlH8QnIr2NRtC1WtjMyK0MdODEbAg9DQ40HtTx1kMQey0rrU0Mlz7Vjey4OzG9SWGhHHaOR23g2n1tAuM4SAMAvvoKvl0hIxwenLPc8BNjUz00gmU1U7bovlgD5x1NeH8phmNAPUVPedE6Xqw49N0iWugPCn1jrKsXspZ0O90UXaSl0s7o4RL3x5njIwyBJKgYjkLY0iGblR6z-L3r1ACi_Qc0GTC9ZMhl7G4HrrNi35OLcLn-QE-neofz37AUNGZB0utG-lV4lxXdmPE-wqIs9r4dyS-O8EV3CBbwbBxkVKQ52Sz2KMYnE-1aq53sNdQ8X1cgQNJU0bGWbmtlbyXReAmpA46wnn2oYzlyfv7NdHMZRCNn4XJ7FJSG5NJLoPtimA2U8YhN_yf2BmG3Bh-Hllbx587pDheRUtesK0Gwsu4K_di5JkRHNVeseFCZPTFEM8qLBPDKXPJ8IJl9BKmTaEzBMVP57bB6nLpnvbeMgIPxVT2VPKmDlGqCuLjYHKtiG3uUbjV5nDed29l1SpuSFZo6e1WsYSrcAIL4d3DGR0qB_aVVsbxD8w_P8DGsMJ_P_sBOekKDAeC9KWi5Zs2SWZrDDbjF6utj65bgZJssrnqycIvYMfYOM0hSXWFdHHGhyxSy5PH6KHjEYocUE7njCdXAaipsVfNFvwG6Rxk6_zISDfgLIRowNGvbLl05t61GhNY1QvhBWOkfKr7jJnCsfeeTjZyi6HBtXn_IjHhZ-jw9qKdpAfKzLyBFJkyCzY_GArproq31SIsJJ1BkAxKNEFR-vFovdrOysOzPsaL60rcCCJZoimOjQ-xnEFqb6yHheFO-HH0JRPfL5OiQD_A_ju6TYRduyNVjJou-D_geHT_8ewHiJCGg_TG0io73GwF0h692apFb8JMCi2BTyLFXJQThEbKBz0IskEqQos5NGIcDs-0-BermtyRsGK2hJQI5tERWJ3YI4o9ZeR9Q87nfbEqMb4MFkLCwaum0e6YIgQnE1O7DKTV5Y7PZpyltBjnkIup4KCFEHWHwuosHBM4pVzh4S7QaOC_Z18non4sFgg3GVoNuscZsKLt01nAlCqcOvHvk2lMyMNcMu0i02o68kKQjSTGDTAonJNk2IvGzu5RctelLfK2RprHza2xcsBxNo4IzTkOp75VomfBfi318tg5Vn62FsW3b2JCCFhLcCFjkuwQYHjFGHcQt941zj-jOnZTG_Eb1Wu9S78-JBCPDc5VcRIL4nsnDH8kyuyMPfm2fwCJRTn697N_ALUZ_82Eou_JK9vC6453xyxq4UCVZtuWsCYGzy2BCVCklB5wmAu3JKk73VbIvDp5QmAebimLMEY1NJNEEx8kLV4wOyLcVkaFATtdA-aTdEz-oAzdJFhurOAmPDtWD6WYT4qlNGK5Gw2wdGymw8nA7yhzmNM2wX83jrYostYcFxmbgIOTsyJBPDnxKzolxsClId1zgyR3bwAxMjJkq0QETYTRk3EbNrBVEhbxTVvXMcjazQtHJxixcFRwCzydAdsJ_FRPWoXWSwu-_0NiLxw-ZOIwEz-x2eEtfYXbBq9F2U3o95wz3JaaadPw9cZH8OWjXZ7q7Y00LQLn8ppPSsMl2g-URRz-8ewH6MiLT5zrcMqABzPFRGkkadZW-83nUxYJCVMKD4U2S_DvQQpx8aHYL3KaO0LZTNXuDYMFTyeoGBK858JxBlX7luXkt4Vww6JscDNnE_jBoo4J9Y3EoERQrjatAbEg_d1zmn6dHC_hKjLomxSwO8PbTmqmIo3gAmSfN8vgNvVZHw2lx-cFcIH7DlkuhorG5xOfBX3ezF6JTuVac9h_aTPM7hyxVjp1RlcZZdLBOAVaIEU_VyLQmQalU7nyMZ-azu2bFhil1vzfH2F0xT7LkGtx6d43ICKOggDktgL_gc-r_nj2A_8B0nVegQ==", 
*   "tag":4387
},
*/

    public class Code
    {
        public Metadata metadata { get; set; }
        public int code_count { get; set; }
        public string code { get; set; }
        public int tag { get; set; }
        public string error { get; set; }
    }

    public class Metadata
    {
        public string id { get; set; }
        public string artist_id { get; set; }
        public String artist { get; set; }
        public string release { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public int bitrate { get; set; }
        public int sample_rate { get; set; }
        public int duration { get; set; }
        public string filename { get; set; }
        public int samples_decoded { get; set; }
        public int given_duration { get; set; }
        public int start_offset { get; set; }
        public string version { get; set; }
        public double codegen_time { get; set; }
        public double decode_time { get; set; }
    }

}
