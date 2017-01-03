using System.Collections.Generic;
using System.Linq;

namespace Past.Common.Data
{
    public class Experience
    {
        public long CharacterXp { get; set; }
        public long GuildXp { get; set; }
        public long JobXp { get; set; }
        public long MountXp { get; set; }
        public long AlignmentXp { get; set; }
        public static Dictionary<byte, Experience> Experiences = new Dictionary<byte, Experience>();

        public Experience(long characterXp, long guildXp, long jobXp, long mountXp, long alignementXp)
        {
            CharacterXp = characterXp;
            GuildXp = guildXp;
            JobXp = jobXp;
            MountXp = mountXp;
            AlignmentXp = alignementXp;
        }

        public static void Initialize()
        {
            Experiences.Add(1, new Experience(0, 0, 0, 0, 0));
            Experiences.Add(2, new Experience(110, 1100, 50, 600, 500));
            Experiences.Add(3, new Experience(650, 6500, 140, 1750, 1500));
            Experiences.Add(4, new Experience(1500, 15000, 271, 2750, 3000));
            Experiences.Add(5, new Experience(2800, 28000, 441, 4000, 5000));
            Experiences.Add(6, new Experience(4800, 48000, 653, 5500, 7500));
            Experiences.Add(7, new Experience(7300, 73000, 905, 7250, 10000));
            Experiences.Add(8, new Experience(10500, 105000, 1199, 9250, 12500));
            Experiences.Add(9, new Experience(14500, 145000, 1543, 11500, 15000));
            Experiences.Add(10, new Experience(19200, 192000, 1911, 14000, 17500));
            Experiences.Add(11, new Experience(25200, 252000, 2330, 16750, -1));
            Experiences.Add(12, new Experience(32600, 326000, 2792, 19750, -1));
            Experiences.Add(13, new Experience(41000, 410000, 3297, 23000, -1));
            Experiences.Add(14, new Experience(50500, 505000, 3840, 26500, -1));
            Experiences.Add(15, new Experience(61000, 610000, 4439, 30250, -1));
            Experiences.Add(16, new Experience(75000, 750000, 5078, 34250, -1));
            Experiences.Add(17, new Experience(91000, 910000, 5762, 38500, -1));
            Experiences.Add(18, new Experience(115000, 1150000, 6493, 43000, -1));
            Experiences.Add(19, new Experience(142000, 1420000, 7280, 47750, -1));
            Experiences.Add(20, new Experience(171000, 1710000, 8097, 52750, -1));
            Experiences.Add(21, new Experience(202000, 2020000, 8980, 58000, -1));
            Experiences.Add(22, new Experience(235000, 2350000, 9898, 63500, -1));
            Experiences.Add(23, new Experience(270000, 2700000, 10875, 69250, -1));
            Experiences.Add(24, new Experience(310000, 3100000, 11903, 75250, -1));
            Experiences.Add(25, new Experience(353000, 3530000, 12985, 81500, -1));
            Experiences.Add(26, new Experience(398500, 3985000, 14122, 88000, -1));
            Experiences.Add(27, new Experience(448000, 4480000, 15315, 94750, -1));
            Experiences.Add(28, new Experience(503000, 5030000, 16564, 101750, -1));
            Experiences.Add(29, new Experience(561000, 5610000, 17873, 109000, -1));
            Experiences.Add(30, new Experience(621600, 6216000, 19242, 116500, -1));
            Experiences.Add(31, new Experience(687000, 6870000, 20672, 124250, -1));
            Experiences.Add(32, new Experience(755000, 7550000, 22166, 132250, -1));
            Experiences.Add(33, new Experience(829000, 8290000, 23726, 140500, -1));
            Experiences.Add(34, new Experience(910000, 9100000, 25353, 149000, -1));
            Experiences.Add(35, new Experience(1000000, 10000000, 27048, 157750, -1));
            Experiences.Add(36, new Experience(1100000, 11000000, 28815, 166750, -1));
            Experiences.Add(37, new Experience(1240000, 12400000, 30656, 176000, -1));
            Experiences.Add(38, new Experience(1400000, 14000000, 32572, 185500, -1));
            Experiences.Add(39, new Experience(1580000, 15800000, 34566, 195250, -1));
            Experiences.Add(40, new Experience(1780000, 17800000, 36641, 205250, -1));
            Experiences.Add(41, new Experience(2000000, 20000000, 38800, 215500, -1));
            Experiences.Add(42, new Experience(2250000, 22500000, 41044, 226000, -1));
            Experiences.Add(43, new Experience(2530000, 25300000, 43378, 236750, -1));
            Experiences.Add(44, new Experience(2850000, 28500000, 45804, 247750, -1));
            Experiences.Add(45, new Experience(3200000, 32000000, 48325, 249000, -1));
            Experiences.Add(46, new Experience(3570000, 35700000, 50946, 270500, -1));
            Experiences.Add(47, new Experience(3960000, 39600000, 53669, 282250, -1));
            Experiences.Add(48, new Experience(4400000, 44000000, 56498, 294250, -1));
            Experiences.Add(49, new Experience(4860000, 48600000, 59437, 306500, -1));
            Experiences.Add(50, new Experience(5350000, 53500000, 62491, 319000, -1));
            Experiences.Add(51, new Experience(5860000, 58600000, 65664, 331750, -1));
            Experiences.Add(52, new Experience(6390000, 63900000, 68960, 344750, -1));
            Experiences.Add(53, new Experience(6950000, 69500000, 72385, 358000, -1));
            Experiences.Add(54, new Experience(7530000, 75300000, 75943, 371500, -1));
            Experiences.Add(55, new Experience(8130000, 81300000, 79640, 385250, -1));
            Experiences.Add(56, new Experience(8765100, 87651000, 83482, 399250, -1));
            Experiences.Add(57, new Experience(9420000, 94200000, 87475, 413500, -1));
            Experiences.Add(58, new Experience(10150000, 101500000, 91624, 428000, -1));
            Experiences.Add(59, new Experience(10894000, 108940000, 95937, 442750, -1));
            Experiences.Add(60, new Experience(11650000, 116500000, 100421, 457750, -1));
            Experiences.Add(61, new Experience(12450000, 124500000, 105082, 473000, -1));
            Experiences.Add(62, new Experience(13280000, 132800000, 109930, 488500, -1));
            Experiences.Add(63, new Experience(14130000, 141300000, 114971, 504250, -1));
            Experiences.Add(64, new Experience(15170000, 151700000, 120215, 520250, -1));
            Experiences.Add(65, new Experience(16251000, 162510000, 125671, 536500, -1));
            Experiences.Add(66, new Experience(17377000, 173770000, 131348, 553000, -1));
            Experiences.Add(67, new Experience(18553000, 185530000, 137256, 569750, -1));
            Experiences.Add(68, new Experience(19778000, 197780000, 143407, 586750, -1));
            Experiences.Add(69, new Experience(21055000, 210550000, 149811, 604000, -1));
            Experiences.Add(70, new Experience(22385000, 223850000, 156481, 621500, -1));
            Experiences.Add(71, new Experience(23529000, 235290000, 163429, 639250, -1));
            Experiences.Add(72, new Experience(25209000, 252090000, 170669, 657250, -1));
            Experiences.Add(73, new Experience(26707000, 267070000, 178214, 675500, -1));
            Experiences.Add(74, new Experience(28264000, 282640000, 186080, 694000, -1));
            Experiences.Add(75, new Experience(29882000, 298820000, 194283, 712750, -1));
            Experiences.Add(76, new Experience(31563000, 315630000, 202839, 731750, -1));
            Experiences.Add(77, new Experience(33307000, 333070000, 211765, 751000, -1));
            Experiences.Add(78, new Experience(35118000, 351180000, 221082, 770500, -1));
            Experiences.Add(79, new Experience(36997000, 369970000, 230808, 790250, -1));
            Experiences.Add(80, new Experience(38945000, 389450000, 240964, 810250, -1));
            Experiences.Add(81, new Experience(40965000, 409650000, 251574, 830500, -1));
            Experiences.Add(82, new Experience(43059000, 430590000, 262660, 851000, -1));
            Experiences.Add(83, new Experience(45229000, 452290000, 274248, 871750, -1));
            Experiences.Add(84, new Experience(47476000, 474760000, 286364, 892750, -1));
            Experiences.Add(85, new Experience(49803000, 498030000, 299037, 914000, -1));
            Experiences.Add(86, new Experience(52211000, 522110000, 312297, 935500, -1));
            Experiences.Add(87, new Experience(54704000, 547040000, 326175, 957250, -1));
            Experiences.Add(88, new Experience(57284000, 572840000, 340705, 979250, -1));
            Experiences.Add(89, new Experience(59952000, 599520000, 355924, 1001500, -1));
            Experiences.Add(90, new Experience(62712000, 627120000, 371870, 1024000, -1));
            Experiences.Add(91, new Experience(65565000, 655650000, 388582, 1046750, -1));
            Experiences.Add(92, new Experience(68514000, 685140000, 406106, 1069750, -1));
            Experiences.Add(93, new Experience(71561000, 715610000, 424486, 1093000, -1));
            Experiences.Add(94, new Experience(74710000, 747100000, 443772, 1116500, -1));
            Experiences.Add(95, new Experience(77963000, 779630000, 464016, 1140250, -1));
            Experiences.Add(96, new Experience(81323000, 813230000, 485274, 1164250, -1));
            Experiences.Add(97, new Experience(84792000, 847920000, 507604, 1188500, -1));
            Experiences.Add(98, new Experience(88374000, 883740000, 531071, 1213000, -1));
            Experiences.Add(99, new Experience(92071000, 920710000, 555541, 1237750, -1));
            Experiences.Add(100, new Experience(95886000, 958860000, 581687, 1262750, -1));
            Experiences.Add(101, new Experience(99823000, 998230000, -1, -1, -1));
            Experiences.Add(102, new Experience(103885000, 1038850000, -1, -1, -1));
            Experiences.Add(103, new Experience(108075000, 1080750000, -1, -1, -1));
            Experiences.Add(104, new Experience(112396000, 1123960000, -1, -1, -1));
            Experiences.Add(105, new Experience(116853000, 1168530000, -1, -1, -1));
            Experiences.Add(106, new Experience(121447000, 1214470000, -1, -1, -1));
            Experiences.Add(107, new Experience(126184000, 1261840000, -1, -1, -1));
            Experiences.Add(108, new Experience(131066000, 1310660000, -1, -1, -1));
            Experiences.Add(109, new Experience(136098000, 1360980000, -1, -1, -1));
            Experiences.Add(110, new Experience(141283000, 1412830000, -1, -1, -1));
            Experiences.Add(111, new Experience(146626000, 1466260000, -1, -1, -1));
            Experiences.Add(112, new Experience(152130000, 1521300000, -1, -1, -1));
            Experiences.Add(113, new Experience(157800000, 1578000000, -1, -1, -1));
            Experiences.Add(114, new Experience(163640000, 1636400000, -1, -1, -1));
            Experiences.Add(115, new Experience(169655000, 1696550000, -1, -1, -1));
            Experiences.Add(116, new Experience(175848000, 1758480000, -1, -1, -1));
            Experiences.Add(117, new Experience(182225000, 1822250000, -1, -1, -1));
            Experiences.Add(118, new Experience(188791000, 1887910000, -1, -1, -1));
            Experiences.Add(119, new Experience(195550000, 1955500000, -1, -1, -1));
            Experiences.Add(120, new Experience(202507000, 2025070000, -1, -1, -1));
            Experiences.Add(121, new Experience(209667000, 2096670000, -1, -1, -1));
            Experiences.Add(122, new Experience(217037000, 2170370000, -1, -1, -1));
            Experiences.Add(123, new Experience(224620000, 2246200000, -1, -1, -1));
            Experiences.Add(124, new Experience(232424000, 2324240000, -1, -1, -1));
            Experiences.Add(125, new Experience(240452000, 2404520000, -1, -1, -1));
            Experiences.Add(126, new Experience(248712000, 2487120000, -1, -1, -1));
            Experiences.Add(127, new Experience(257209000, 2572090000, -1, -1, -1));
            Experiences.Add(128, new Experience(265949000, 2659490000, -1, -1, -1));
            Experiences.Add(129, new Experience(274939000, 2749390000, -1, -1, -1));
            Experiences.Add(130, new Experience(284186000, 2841860000, -1, -1, -1));
            Experiences.Add(131, new Experience(293694000, 2936940000, -1, -1, -1));
            Experiences.Add(132, new Experience(303473000, 3034730000, -1, -1, -1));
            Experiences.Add(133, new Experience(313527000, 3135270000, -1, -1, -1));
            Experiences.Add(134, new Experience(323866000, 3238660000, -1, -1, -1));
            Experiences.Add(135, new Experience(334495000, 3344950000, -1, -1, -1));
            Experiences.Add(136, new Experience(345423000, 3454230000, -1, -1, -1));
            Experiences.Add(137, new Experience(356657000, 3566570000, -1, -1, -1));
            Experiences.Add(138, new Experience(368206000, 3682060000, -1, -1, -1));
            Experiences.Add(139, new Experience(380076000, 3800760000, -1, -1, -1));
            Experiences.Add(140, new Experience(392278000, 3922780000, -1, -1, -1));
            Experiences.Add(141, new Experience(404818000, 4048180000, -1, -1, -1));
            Experiences.Add(142, new Experience(417706000, 4177060000, -1, -1, -1));
            Experiences.Add(143, new Experience(430952000, 4309520000, -1, -1, -1));
            Experiences.Add(144, new Experience(444564000, 4445640000, -1, -1, -1));
            Experiences.Add(145, new Experience(458551000, 4585510000, -1, -1, -1));
            Experiences.Add(146, new Experience(472924000, 4729240000, -1, -1, -1));
            Experiences.Add(147, new Experience(487693000, 4876930000, -1, -1, -1));
            Experiences.Add(148, new Experience(502867000, 5028670000, -1, -1, -1));
            Experiences.Add(149, new Experience(518458000, 5184580000, -1, -1, -1));
            Experiences.Add(150, new Experience(534476000, 5344760000, -1, -1, -1));
            Experiences.Add(151, new Experience(502867000, 5028670000, -1, -1, -1));
            Experiences.Add(152, new Experience(567839000, 5678390000, -1, -1, -1));
            Experiences.Add(153, new Experience(585206000, 5852060000, -1, -1, -1));
            Experiences.Add(154, new Experience(603047000, 6030470000, -1, -1, -1));
            Experiences.Add(155, new Experience(621374000, 6213740000, -1, -1, -1));
            Experiences.Add(156, new Experience(640199000, 6401990000, -1, -1, -1));
            Experiences.Add(157, new Experience(659536000, 6595360000, -1, -1, -1));
            Experiences.Add(158, new Experience(679398000, 6793980000, -1, -1, -1));
            Experiences.Add(159, new Experience(699798000, 6997980000, -1, -1, -1));
            Experiences.Add(160, new Experience(720751000, 7207510000, -1, -1, -1));
            Experiences.Add(161, new Experience(742772000, 7427720000, -1, -1, -1));
            Experiences.Add(162, new Experience(764374000, 7643740000, -1, -1, -1));
            Experiences.Add(163, new Experience(787074000, 7870740000, -1, -1, -1));
            Experiences.Add(164, new Experience(810387000, 8103870000, -1, -1, -1));
            Experiences.Add(165, new Experience(834329000, 8343290000, -1, -1, -1));
            Experiences.Add(166, new Experience(858917000, 8589170000, -1, -1, -1));
            Experiences.Add(167, new Experience(884167000, 8841670000, -1, -1, -1));
            Experiences.Add(168, new Experience(910098000, 9100980000, -1, -1, -1));
            Experiences.Add(169, new Experience(936727000, 9367270000, -1, -1, -1));
            Experiences.Add(170, new Experience(964073000, 9640730000, -1, -1, -1));
            Experiences.Add(171, new Experience(992154000, 9921540000, -1, -1, -1));
            Experiences.Add(172, new Experience(1020991000, 10209910000, -1, -1, -1));
            Experiences.Add(173, new Experience(1050603000, 10506030000, -1, -1, -1));
            Experiences.Add(174, new Experience(1081010000, 10810100000, -1, -1, -1));
            Experiences.Add(175, new Experience(1112235000, 11122350000, -1, -1, -1));
            Experiences.Add(176, new Experience(1144298000, 11442980000, -1, -1, -1));
            Experiences.Add(177, new Experience(1177222000, 11772220000, -1, -1, -1));
            Experiences.Add(178, new Experience(1211030000, 12110300000, -1, -1, -1));
            Experiences.Add(179, new Experience(1245745000, 12457450000, -1, -1, -1));
            Experiences.Add(180, new Experience(1281393000, 12813930000, -1, -1, -1));
            Experiences.Add(181, new Experience(1317997000, 13179970000, -1, -1, -1));
            Experiences.Add(182, new Experience(1355584000, 13555840000, -1, -1, -1));
            Experiences.Add(183, new Experience(1404179000, 14041790000, -1, -1, -1));
            Experiences.Add(184, new Experience(1463811000, 14638110000, -1, -1, -1));
            Experiences.Add(185, new Experience(1534506000, 15345060000, -1, -1, -1));
            Experiences.Add(186, new Experience(1616294000, 16162940000, -1, -1, -1));
            Experiences.Add(187, new Experience(1709205000, 17092050000, -1, -1, -1));
            Experiences.Add(188, new Experience(1813267000, 18132670000, -1, -1, -1));
            Experiences.Add(189, new Experience(1928513000, 19285130000, -1, -1, -1));
            Experiences.Add(190, new Experience(2054975000, 20549750000, -1, -1, -1));
            Experiences.Add(191, new Experience(2192686000, 21926860000, -1, -1, -1));
            Experiences.Add(192, new Experience(2341679000, 23416790000, -1, -1, -1));
            Experiences.Add(193, new Experience(2501990000, 25019900000, -1, -1, -1));
            Experiences.Add(194, new Experience(2673655000, 26736550000, -1, -1, -1));
            Experiences.Add(195, new Experience(2856710000, 28567100000, -1, -1, -1));
            Experiences.Add(196, new Experience(3051194000, 30511940000, -1, -1, -1));
            Experiences.Add(197, new Experience(3257146000, 32571460000, -1, -1, -1));
            Experiences.Add(198, new Experience(3474606000, 34746060000, -1, -1, -1));
            Experiences.Add(199, new Experience(3703616000, 37036160000, -1, -1, -1));
            Experiences.Add(200, new Experience(7407232000, 74072320000, -1, -1, -1));
        }

        public static Experience GetExperienceLevelFloor(byte level)
        {
            return Experiences.FirstOrDefault(x => x.Key == level).Value;
        }

        public static Experience GetNextExperienceLevelFloor(byte level)
        {
            return Experiences.FirstOrDefault(x => x.Key == level + 1).Value;
        }

        public static byte GetCharacterGrade(ushort honor)
        {
            if (honor >= 17500)
            {
                return 10;
            }
            return (byte)(Experiences.FirstOrDefault(x => x.Value.AlignmentXp > honor).Key - 1);
        }
    }
}
