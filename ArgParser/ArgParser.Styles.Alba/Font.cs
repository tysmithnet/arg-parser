// ***********************************************************************
// Assembly         : ArgParser.Styles.Alba
// Author           : @tysmithnet
// Created          : 11-20-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="Font.cs" company="tysmith.net">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Colorful;

namespace ArgParser.Styles.Alba
{
    /// <summary>
    ///     Provided figlet fonts
    /// </summary>
    public abstract class Font
    {
        /// <summary>
        ///     The acrobatic
        /// </summary>
        public static readonly Font Acrobatic = new _Acrobatic();

        /// <summary>
        ///     The alligator
        /// </summary>
        public static readonly Font Alligator = new _Alligator();

        /// <summary>
        ///     The alligator2
        /// </summary>
        public static readonly Font Alligator2 = new _Alligator2();

        /// <summary>
        ///     The alligator3
        /// </summary>
        public static readonly Font Alligator3 = new _Alligator3();

        /// <summary>
        ///     The alpha
        /// </summary>
        public static readonly Font Alpha = new _Alpha();

        /// <summary>
        ///     The alphabet
        /// </summary>
        public static readonly Font Alphabet = new _Alphabet();

        /// <summary>
        ///     The amc3line
        /// </summary>
        public static readonly Font Amc3line = new _Amc3line();

        /// <summary>
        ///     The amc3liv1
        /// </summary>
        public static readonly Font Amc3liv1 = new _Amc3liv1();

        /// <summary>
        ///     The amcaaa01
        /// </summary>
        public static readonly Font Amcaaa01 = new _Amcaaa01();

        /// <summary>
        ///     The amcneko
        /// </summary>
        public static readonly Font Amcneko = new _Amcneko();

        /// <summary>
        ///     The amcrazo2
        /// </summary>
        public static readonly Font Amcrazo2 = new _Amcrazo2();

        /// <summary>
        ///     The amcrazor
        /// </summary>
        public static readonly Font Amcrazor = new _Amcrazor();

        /// <summary>
        ///     The amcslash
        /// </summary>
        public static readonly Font Amcslash = new _Amcslash();

        /// <summary>
        ///     The amcslder
        /// </summary>
        public static readonly Font Amcslder = new _Amcslder();

        /// <summary>
        ///     The amcthin
        /// </summary>
        public static readonly Font Amcthin = new _Amcthin();

        /// <summary>
        ///     The amctubes
        /// </summary>
        public static readonly Font Amctubes = new _Amctubes();

        /// <summary>
        ///     The amcun1
        /// </summary>
        public static readonly Font Amcun1 = new _Amcun1();

        /// <summary>
        ///     The arrows
        /// </summary>
        public static readonly Font Arrows = new _Arrows();

        /// <summary>
        ///     The ASCII new roman
        /// </summary>
        public static readonly Font AsciiNewRoman = new _AsciiNewRoman();

        /// <summary>
        ///     The avatar
        /// </summary>
        public static readonly Font Avatar = new _Avatar();

        /// <summary>
        ///     The b1 ff
        /// </summary>
        public static readonly Font B1FF = new _B1FF();

        /// <summary>
        ///     The banner
        /// </summary>
        public static readonly Font Banner = new _Banner();

        /// <summary>
        ///     The banner3
        /// </summary>
        public static readonly Font Banner3 = new _Banner3();

        /// <summary>
        ///     The banner3 d
        /// </summary>
        public static readonly Font Banner3D = new _Banner3D();

        /// <summary>
        ///     The banner4
        /// </summary>
        public static readonly Font Banner4 = new _Banner4();

        /// <summary>
        ///     The barbwire
        /// </summary>
        public static readonly Font Barbwire = new _Barbwire();

        /// <summary>
        ///     The basic
        /// </summary>
        public static readonly Font Basic = new _Basic();

        /// <summary>
        ///     The bear
        /// </summary>
        public static readonly Font Bear = new _Bear();

        /// <summary>
        ///     The bell
        /// </summary>
        public static readonly Font Bell = new _Bell();

        /// <summary>
        ///     The benjamin
        /// </summary>
        public static readonly Font Benjamin = new _Benjamin();

        /// <summary>
        ///     The big
        /// </summary>
        public static readonly Font Big = new _Big();

        /// <summary>
        ///     The bigchief
        /// </summary>
        public static readonly Font Bigchief = new _Bigchief();

        /// <summary>
        ///     The bigfig
        /// </summary>
        public static readonly Font Bigfig = new _Bigfig();

        /// <summary>
        ///     The binary
        /// </summary>
        public static readonly Font Binary = new _Binary();

        /// <summary>
        ///     The block
        /// </summary>
        public static readonly Font Block = new _Block();

        /// <summary>
        ///     The blocks
        /// </summary>
        public static readonly Font Blocks = new _Blocks();

        /// <summary>
        ///     The bolger
        /// </summary>
        public static readonly Font Bolger = new _Bolger();

        /// <summary>
        ///     The braced
        /// </summary>
        public static readonly Font Braced = new _Braced();

        /// <summary>
        ///     The bright
        /// </summary>
        public static readonly Font Bright = new _Bright();

        /// <summary>
        ///     The broadway
        /// </summary>
        public static readonly Font Broadway = new _Broadway();

        /// <summary>
        ///     The broadway kb
        /// </summary>
        public static readonly Font BroadwayKb = new _BroadwayKb();

        /// <summary>
        ///     The bubble
        /// </summary>
        public static readonly Font Bubble = new _Bubble();

        /// <summary>
        ///     The bulbhead
        /// </summary>
        public static readonly Font Bulbhead = new _Bulbhead();

        /// <summary>
        ///     The calgphy2
        /// </summary>
        public static readonly Font Calgphy2 = new _Calgphy2();

        /// <summary>
        ///     The caligraphy
        /// </summary>
        public static readonly Font Caligraphy = new _Caligraphy();

        /// <summary>
        ///     The cards
        /// </summary>
        public static readonly Font Cards = new _Cards();

        /// <summary>
        ///     The catwalk
        /// </summary>
        public static readonly Font Catwalk = new _Catwalk();

        /// <summary>
        ///     The chiseled
        /// </summary>
        public static readonly Font Chiseled = new _Chiseled();

        /// <summary>
        ///     The chunky
        /// </summary>
        public static readonly Font Chunky = new _Chunky();

        /// <summary>
        ///     The coinstak
        /// </summary>
        public static readonly Font Coinstak = new _Coinstak();

        /// <summary>
        ///     The cola
        /// </summary>
        public static readonly Font Cola = new _Cola();

        /// <summary>
        ///     The colossal
        /// </summary>
        public static readonly Font Colossal = new _Colossal();

        /// <summary>
        ///     The computer
        /// </summary>
        public static readonly Font Computer = new _Computer();

        /// <summary>
        ///     The contessa
        /// </summary>
        public static readonly Font Contessa = new _Contessa();

        /// <summary>
        ///     The contrast
        /// </summary>
        public static readonly Font Contrast = new _Contrast();

        /// <summary>
        ///     The cosmic
        /// </summary>
        public static readonly Font Cosmic = new _Cosmic();

        /// <summary>
        ///     The cosmike
        /// </summary>
        public static readonly Font Cosmike = new _Cosmike();

        /// <summary>
        ///     The crawford
        /// </summary>
        public static readonly Font Crawford = new _Crawford();

        /// <summary>
        ///     The crazy
        /// </summary>
        public static readonly Font Crazy = new _Crazy();

        /// <summary>
        ///     The cricket
        /// </summary>
        public static readonly Font Cricket = new _Cricket();

        /// <summary>
        ///     The cyberlarge
        /// </summary>
        public static readonly Font Cyberlarge = new _Cyberlarge();

        /// <summary>
        ///     The cybermedium
        /// </summary>
        public static readonly Font Cybermedium = new _Cybermedium();

        /// <summary>
        ///     The cybersmall
        /// </summary>
        public static readonly Font Cybersmall = new _Cybersmall();

        /// <summary>
        ///     The cygnet
        /// </summary>
        public static readonly Font Cygnet = new _Cygnet();

        /// <summary>
        ///     The dan c4
        /// </summary>
        public static readonly Font DANC4 = new _DANC4();

        /// <summary>
        ///     The dancingfont
        /// </summary>
        public static readonly Font Dancingfont = new _Dancingfont();

        /// <summary>
        ///     The decimal
        /// </summary>
        public static readonly Font Decimal = new _Decimal();

        /// <summary>
        ///     The defleppard
        /// </summary>
        public static readonly Font Defleppard = new _Defleppard();

        /// <summary>
        ///     The diamond
        /// </summary>
        public static readonly Font Diamond = new _Diamond();

        /// <summary>
        ///     The dietcola
        /// </summary>
        public static readonly Font Dietcola = new _Dietcola();

        /// <summary>
        ///     The digital
        /// </summary>
        public static readonly Font Digital = new _Digital();

        /// <summary>
        ///     The doh
        /// </summary>
        public static readonly Font Doh = new _Doh();

        /// <summary>
        ///     The doom
        /// </summary>
        public static readonly Font Doom = new _Doom();

        /// <summary>
        ///     The dosrebel
        /// </summary>
        public static readonly Font Dosrebel = new _Dosrebel();

        /// <summary>
        ///     The dotmatrix
        /// </summary>
        public static readonly Font Dotmatrix = new _Dotmatrix();

        /// <summary>
        ///     The double
        /// </summary>
        public static readonly Font Double = new _Double();

        /// <summary>
        ///     The doubleshorts
        /// </summary>
        public static readonly Font Doubleshorts = new _Doubleshorts();

        /// <summary>
        ///     The drpepper
        /// </summary>
        public static readonly Font Drpepper = new _Drpepper();

        /// <summary>
        ///     The dwhistled
        /// </summary>
        public static readonly Font Dwhistled = new _Dwhistled();

        /// <summary>
        ///     The eftichess
        /// </summary>
        public static readonly Font Eftichess = new _Eftichess();

        /// <summary>
        ///     The eftifont
        /// </summary>
        public static readonly Font Eftifont = new _Eftifont();

        /// <summary>
        ///     The eftipiti
        /// </summary>
        public static readonly Font Eftipiti = new _Eftipiti();

        /// <summary>
        ///     The eftirobot
        /// </summary>
        public static readonly Font Eftirobot = new _Eftirobot();

        /// <summary>
        ///     The eftitalic
        /// </summary>
        public static readonly Font Eftitalic = new _Eftitalic();

        /// <summary>
        ///     The eftiwall
        /// </summary>
        public static readonly Font Eftiwall = new _Eftiwall();

        /// <summary>
        ///     The eftiwater
        /// </summary>
        public static readonly Font Eftiwater = new _Eftiwater();

        /// <summary>
        ///     The epic
        /// </summary>
        public static readonly Font Epic = new _Epic();

        /// <summary>
        ///     The fender
        /// </summary>
        public static readonly Font Fender = new _Fender();

        /// <summary>
        ///     The filter
        /// </summary>
        public static readonly Font Filter = new _Filter();

        /// <summary>
        ///     The fire fontk
        /// </summary>
        public static readonly Font FireFontk = new _FireFontk();

        /// <summary>
        ///     The fire fonts
        /// </summary>
        public static readonly Font FireFonts = new _FireFonts();

        /// <summary>
        ///     The five line oblique
        /// </summary>
        public static readonly Font FiveLineOblique = new _5lineoblique();

        /// <summary>
        ///     The flipped
        /// </summary>
        public static readonly Font Flipped = new _Flipped();

        /// <summary>
        ///     The flowerpower
        /// </summary>
        public static readonly Font Flowerpower = new _Flowerpower();

        /// <summary>
        ///     The four maximum
        /// </summary>
        public static readonly Font FourMax = new _4max();

        /// <summary>
        ///     The fourtops
        /// </summary>
        public static readonly Font Fourtops = new _Fourtops();

        /// <summary>
        ///     The fraktur
        /// </summary>
        public static readonly Font Fraktur = new _Fraktur();

        /// <summary>
        ///     The funface
        /// </summary>
        public static readonly Font Funface = new _Funface();

        /// <summary>
        ///     The funfaces
        /// </summary>
        public static readonly Font Funfaces = new _Funfaces();

        /// <summary>
        ///     The fuzzy
        /// </summary>
        public static readonly Font Fuzzy = new _Fuzzy();

        /// <summary>
        ///     The georgi16
        /// </summary>
        public static readonly Font Georgi16 = new _Georgi16();

        /// <summary>
        ///     The georgia11
        /// </summary>
        public static readonly Font Georgia11 = new _Georgia11();

        /// <summary>
        ///     The ghost
        /// </summary>
        public static readonly Font Ghost = new _Ghost();

        /// <summary>
        ///     The ghoulish
        /// </summary>
        public static readonly Font Ghoulish = new _Ghoulish();

        /// <summary>
        ///     The glenyn
        /// </summary>
        public static readonly Font Glenyn = new _Glenyn();

        /// <summary>
        ///     The goofy
        /// </summary>
        public static readonly Font Goofy = new _Goofy();

        /// <summary>
        ///     The gothic
        /// </summary>
        public static readonly Font Gothic = new _Gothic();

        /// <summary>
        ///     The graceful
        /// </summary>
        public static readonly Font Graceful = new _Graceful();

        /// <summary>
        ///     The gradient
        /// </summary>
        public static readonly Font Gradient = new _Gradient();

        /// <summary>
        ///     The graffiti
        /// </summary>
        public static readonly Font Graffiti = new _Graffiti();

        /// <summary>
        ///     The greek
        /// </summary>
        public static readonly Font Greek = new _Greek();

        /// <summary>
        ///     The heart left
        /// </summary>
        public static readonly Font HeartLeft = new _HeartLeft();

        /// <summary>
        ///     The heart right
        /// </summary>
        public static readonly Font HeartRight = new _HeartRight();

        /// <summary>
        ///     The henry3d
        /// </summary>
        public static readonly Font Henry3d = new _Henry3d();

        /// <summary>
        ///     The hexadecimal
        /// </summary>
        public static readonly Font Hex = new _Hex();

        /// <summary>
        ///     The hieroglyphs
        /// </summary>
        public static readonly Font Hieroglyphs = new _Hieroglyphs();

        /// <summary>
        ///     The hollywood
        /// </summary>
        public static readonly Font Hollywood = new _Hollywood();

        /// <summary>
        ///     The horizontalleft
        /// </summary>
        public static readonly Font Horizontalleft = new _Horizontalleft();

        /// <summary>
        ///     The horizontalright
        /// </summary>
        public static readonly Font Horizontalright = new _Horizontalright();

        /// <summary>
        ///     The ic L1900
        /// </summary>
        public static readonly Font ICL1900 = new _ICL1900();

        /// <summary>
        ///     The impossible
        /// </summary>
        public static readonly Font Impossible = new _Impossible();

        /// <summary>
        ///     The invita
        /// </summary>
        public static readonly Font Invita = new _Invita();

        /// <summary>
        ///     The isometric1
        /// </summary>
        public static readonly Font Isometric1 = new _Isometric1();

        /// <summary>
        ///     The isometric2
        /// </summary>
        public static readonly Font Isometric2 = new _Isometric2();

        /// <summary>
        ///     The isometric3
        /// </summary>
        public static readonly Font Isometric3 = new _Isometric3();

        /// <summary>
        ///     The isometric4
        /// </summary>
        public static readonly Font Isometric4 = new _Isometric4();

        /// <summary>
        ///     The italic
        /// </summary>
        public static readonly Font Italic = new _Italic();

        /// <summary>
        ///     The ivrit
        /// </summary>
        public static readonly Font Ivrit = new _Ivrit();

        /// <summary>
        ///     The jacky
        /// </summary>
        public static readonly Font Jacky = new _Jacky();

        /// <summary>
        ///     The jazmine
        /// </summary>
        public static readonly Font Jazmine = new _Jazmine();

        /// <summary>
        ///     The jerusalem
        /// </summary>
        public static readonly Font Jerusalem = new _Jerusalem();

        /// <summary>
        ///     The katakana
        /// </summary>
        public static readonly Font Katakana = new _Katakana();

        /// <summary>
        ///     The kban
        /// </summary>
        public static readonly Font Kban = new _Kban();

        /// <summary>
        ///     The keyboard
        /// </summary>
        public static readonly Font Keyboard = new _Keyboard();

        /// <summary>
        ///     The knob
        /// </summary>
        public static readonly Font Knob = new _Knob();

        /// <summary>
        ///     The konto
        /// </summary>
        public static readonly Font Konto = new _Konto();

        /// <summary>
        ///     The kontoslant
        /// </summary>
        public static readonly Font Kontoslant = new _Kontoslant();

        /// <summary>
        ///     The larry3d
        /// </summary>
        public static readonly Font Larry3d = new _Larry3d();

        /// <summary>
        ///     The LCD
        /// </summary>
        public static readonly Font Lcd = new _Lcd();

        /// <summary>
        ///     The lean
        /// </summary>
        public static readonly Font Lean = new _Lean();

        /// <summary>
        ///     The letters
        /// </summary>
        public static readonly Font Letters = new _Letters();

        /// <summary>
        ///     The lildevil
        /// </summary>
        public static readonly Font Lildevil = new _Lildevil();

        /// <summary>
        ///     The lineblocks
        /// </summary>
        public static readonly Font Lineblocks = new _Lineblocks();

        /// <summary>
        ///     The linux
        /// </summary>
        public static readonly Font Linux = new _Linux();

        /// <summary>
        ///     The lockergnome
        /// </summary>
        public static readonly Font Lockergnome = new _Lockergnome();

        /// <summary>
        ///     The madrid
        /// </summary>
        public static readonly Font Madrid = new _Madrid();

        /// <summary>
        ///     The marquee
        /// </summary>
        public static readonly Font Marquee = new _Marquee();

        /// <summary>
        ///     The maxfour
        /// </summary>
        public static readonly Font Maxfour = new _Maxfour();

        /// <summary>
        ///     The merlin1
        /// </summary>
        public static readonly Font Merlin1 = new _Merlin1();

        /// <summary>
        ///     The merlin2
        /// </summary>
        public static readonly Font Merlin2 = new _Merlin2();

        /// <summary>
        ///     The mike
        /// </summary>
        public static readonly Font Mike = new _Mike();

        /// <summary>
        ///     The mini
        /// </summary>
        public static readonly Font Mini = new _Mini();

        /// <summary>
        ///     The mirror
        /// </summary>
        public static readonly Font Mirror = new _Mirror();

        /// <summary>
        ///     The mnemonic
        /// </summary>
        public static readonly Font Mnemonic = new _Mnemonic();

        /// <summary>
        ///     The modular
        /// </summary>
        public static readonly Font Modular = new _Modular();

        /// <summary>
        ///     The morse
        /// </summary>
        public static readonly Font Morse = new _Morse();

        /// <summary>
        ///     The morse2
        /// </summary>
        public static readonly Font Morse2 = new _Morse2();

        /// <summary>
        ///     The moscow
        /// </summary>
        public static readonly Font Moscow = new _Moscow();

        /// <summary>
        ///     The mshebrew210
        /// </summary>
        public static readonly Font Mshebrew210 = new _Mshebrew210();

        /// <summary>
        ///     The muzzle
        /// </summary>
        public static readonly Font Muzzle = new _Muzzle();

        /// <summary>
        ///     The nancyj
        /// </summary>
        public static readonly Font Nancyj = new _Nancyj();

        /// <summary>
        ///     The nancyjfancy
        /// </summary>
        public static readonly Font Nancyjfancy = new _Nancyjfancy();

        /// <summary>
        ///     The nancyjimproved
        /// </summary>
        public static readonly Font Nancyjimproved = new _Nancyjimproved();

        /// <summary>
        ///     The nancyjunderlined
        /// </summary>
        public static readonly Font Nancyjunderlined = new _Nancyjunderlined();

        /// <summary>
        ///     The nipples
        /// </summary>
        public static readonly Font Nipples = new _Nipples();

        /// <summary>
        ///     The nscript
        /// </summary>
        public static readonly Font Nscript = new _Nscript();

        /// <summary>
        ///     The ntgreek
        /// </summary>
        public static readonly Font Ntgreek = new _Ntgreek();

        /// <summary>
        ///     The nvscript
        /// </summary>
        public static readonly Font Nvscript = new _Nvscript();

        /// <summary>
        ///     The o8
        /// </summary>
        public static readonly Font O8 = new _O8();

        /// <summary>
        ///     The octal
        /// </summary>
        public static readonly Font Octal = new _Octal();

        /// <summary>
        ///     The ogre
        /// </summary>
        public static readonly Font Ogre = new _Ogre();

        /// <summary>
        ///     The oldbanner
        /// </summary>
        public static readonly Font Oldbanner = new _Oldbanner();

        /// <summary>
        ///     The one row
        /// </summary>
        public static readonly Font OneRow = new _1row();

        /// <summary>
        ///     The os2
        /// </summary>
        public static readonly Font Os2 = new _Os2();

        /// <summary>
        ///     The pawp
        /// </summary>
        public static readonly Font Pawp = new _Pawp();

        /// <summary>
        ///     The peaks
        /// </summary>
        public static readonly Font Peaks = new _Peaks();

        /// <summary>
        ///     The peaksslant
        /// </summary>
        public static readonly Font Peaksslant = new _Peaksslant();

        /// <summary>
        ///     The pebbles
        /// </summary>
        public static readonly Font Pebbles = new _Pebbles();

        /// <summary>
        ///     The pepper
        /// </summary>
        public static readonly Font Pepper = new _Pepper();

        /// <summary>
        ///     The poison
        /// </summary>
        public static readonly Font Poison = new _Poison();

        /// <summary>
        ///     The puffy
        /// </summary>
        public static readonly Font Puffy = new _Puffy();

        /// <summary>
        ///     The puzzle
        /// </summary>
        public static readonly Font Puzzle = new _Puzzle();

        /// <summary>
        ///     The pyramid
        /// </summary>
        public static readonly Font Pyramid = new _Pyramid();

        /// <summary>
        ///     The rammstein
        /// </summary>
        public static readonly Font Rammstein = new _Rammstein();

        /// <summary>
        ///     The rectangles
        /// </summary>
        public static readonly Font Rectangles = new _Rectangles();

        /// <summary>
        ///     The red phoenix
        /// </summary>
        public static readonly Font RedPhoenix = new _RedPhoenix();

        /// <summary>
        ///     The relief
        /// </summary>
        public static readonly Font Relief = new _Relief();

        /// <summary>
        ///     The relief2
        /// </summary>
        public static readonly Font Relief2 = new _Relief2();

        /// <summary>
        ///     The reverse
        /// </summary>
        public static readonly Font Reverse = new _Reverse();

        /// <summary>
        ///     The roman
        /// </summary>
        public static readonly Font Roman = new _Roman();

        /// <summary>
        ///     The rot13
        /// </summary>
        public static readonly Font Rot13 = new _Rot13();

        /// <summary>
        ///     The rotated
        /// </summary>
        public static readonly Font Rotated = new _Rotated();

        /// <summary>
        ///     The rounded
        /// </summary>
        public static readonly Font Rounded = new _Rounded();

        /// <summary>
        ///     The rowancap
        /// </summary>
        public static readonly Font Rowancap = new _Rowancap();

        /// <summary>
        ///     The rozzo
        /// </summary>
        public static readonly Font Rozzo = new _Rozzo();

        /// <summary>
        ///     The runic
        /// </summary>
        public static readonly Font Runic = new _Runic();

        /// <summary>
        ///     The runyc
        /// </summary>
        public static readonly Font Runyc = new _Runyc();

        /// <summary>
        ///     The santaclara
        /// </summary>
        public static readonly Font Santaclara = new _Santaclara();

        /// <summary>
        ///     The sblood
        /// </summary>
        public static readonly Font Sblood = new _Sblood();

        /// <summary>
        ///     The script
        /// </summary>
        public static readonly Font Script = new _Script();

        /// <summary>
        ///     The serifcap
        /// </summary>
        public static readonly Font Serifcap = new _Serifcap();

        /// <summary>
        ///     The shadow
        /// </summary>
        public static readonly Font Shadow = new _Shadow();

        /// <summary>
        ///     The shimrod
        /// </summary>
        public static readonly Font Shimrod = new _Shimrod();

        /// <summary>
        ///     The short
        /// </summary>
        public static readonly Font Short = new _Short();

        /// <summary>
        ///     The slant
        /// </summary>
        public static readonly Font Slant = new _Slant();

        /// <summary>
        ///     The slide
        /// </summary>
        public static readonly Font Slide = new _Slide();

        /// <summary>
        ///     The slscript
        /// </summary>
        public static readonly Font Slscript = new _Slscript();

        /// <summary>
        ///     The small
        /// </summary>
        public static readonly Font Small = new _Small();

        /// <summary>
        ///     The smallcaps
        /// </summary>
        public static readonly Font Smallcaps = new _Smallcaps();

        /// <summary>
        ///     The smisome1
        /// </summary>
        public static readonly Font Smisome1 = new _Smisome1();

        /// <summary>
        ///     The smkeyboard
        /// </summary>
        public static readonly Font Smkeyboard = new _Smkeyboard();

        /// <summary>
        ///     The smpoison
        /// </summary>
        public static readonly Font Smpoison = new _Smpoison();

        /// <summary>
        ///     The smscript
        /// </summary>
        public static readonly Font Smscript = new _Smscript();

        /// <summary>
        ///     The smshadow
        /// </summary>
        public static readonly Font Smshadow = new _Smshadow();

        /// <summary>
        ///     The smslant
        /// </summary>
        public static readonly Font Smslant = new _Smslant();

        /// <summary>
        ///     The smtengwar
        /// </summary>
        public static readonly Font Smtengwar = new _Smtengwar();

        /// <summary>
        ///     The soft
        /// </summary>
        public static readonly Font Soft = new _Soft();

        /// <summary>
        ///     The speed
        /// </summary>
        public static readonly Font Speed = new _Speed();

        /// <summary>
        ///     The spliff
        /// </summary>
        public static readonly Font Spliff = new _Spliff();

        /// <summary>
        ///     The srelief
        /// </summary>
        public static readonly Font Srelief = new _Srelief();

        /// <summary>
        ///     The stacey
        /// </summary>
        public static readonly Font Stacey = new _Stacey();

        /// <summary>
        ///     The stampate
        /// </summary>
        public static readonly Font Stampate = new _Stampate();

        /// <summary>
        ///     The stampatello
        /// </summary>
        public static readonly Font Stampatello = new _Stampatello();

        /// <summary>
        ///     The standard
        /// </summary>
        public static readonly Font Standard = new _Standard();

        /// <summary>
        ///     The starstrips
        /// </summary>
        public static readonly Font Starstrips = new _Starstrips();

        /// <summary>
        ///     The starwars
        /// </summary>
        public static readonly Font Starwars = new _Starwars();

        /// <summary>
        ///     The stellar
        /// </summary>
        public static readonly Font Stellar = new _Stellar();

        /// <summary>
        ///     The stforek
        /// </summary>
        public static readonly Font Stforek = new _Stforek();

        /// <summary>
        ///     The stop
        /// </summary>
        public static readonly Font Stop = new _Stop();

        /// <summary>
        ///     The straight
        /// </summary>
        public static readonly Font Straight = new _Straight();

        /// <summary>
        ///     The subzero
        /// </summary>
        public static readonly Font Subzero = new _Subzero();

        /// <summary>
        ///     The swampland
        /// </summary>
        public static readonly Font Swampland = new _Swampland();

        /// <summary>
        ///     The swan
        /// </summary>
        public static readonly Font Swan = new _Swan();

        /// <summary>
        ///     The sweet
        /// </summary>
        public static readonly Font Sweet = new _Sweet();

        /// <summary>
        ///     The tanja
        /// </summary>
        public static readonly Font Tanja = new _Tanja();

        /// <summary>
        ///     The tengwar
        /// </summary>
        public static readonly Font Tengwar = new _Tengwar();

        /// <summary>
        ///     The term
        /// </summary>
        public static readonly Font Term = new _Term();

        /// <summary>
        ///     The test1
        /// </summary>
        public static readonly Font Test1 = new _Test1();

        /// <summary>
        ///     The thick
        /// </summary>
        public static readonly Font Thick = new _Thick();

        /// <summary>
        ///     The thin
        /// </summary>
        public static readonly Font Thin = new _Thin();

        /// <summary>
        ///     The three d
        /// </summary>
        public static readonly Font ThreeD = new _3d();

        /// <summary>
        ///     The three d diagonal
        /// </summary>
        public static readonly Font ThreeDDiagonal = new _3dDiagonal();

        /// <summary>
        ///     The threepoint
        /// </summary>
        public static readonly Font Threepoint = new _Threepoint();

        /// <summary>
        ///     The three x five
        /// </summary>
        public static readonly Font ThreeXFive = new _3x5();

        /// <summary>
        ///     The ticks
        /// </summary>
        public static readonly Font Ticks = new _Ticks();

        /// <summary>
        ///     The ticksslant
        /// </summary>
        public static readonly Font Ticksslant = new _Ticksslant();

        /// <summary>
        ///     The tiles
        /// </summary>
        public static readonly Font Tiles = new _Tiles();

        /// <summary>
        ///     The tinkertoy
        /// </summary>
        public static readonly Font Tinkertoy = new _Tinkertoy();

        /// <summary>
        ///     The tombstone
        /// </summary>
        public static readonly Font Tombstone = new _Tombstone();

        /// <summary>
        ///     The train
        /// </summary>
        public static readonly Font Train = new _Train();

        /// <summary>
        ///     The trek
        /// </summary>
        public static readonly Font Trek = new _Trek();

        /// <summary>
        ///     The tsalagi
        /// </summary>
        public static readonly Font Tsalagi = new _Tsalagi();

        /// <summary>
        ///     The tubular
        /// </summary>
        public static readonly Font Tubular = new _Tubular();

        /// <summary>
        ///     The twisted
        /// </summary>
        public static readonly Font Twisted = new _Twisted();

        /// <summary>
        ///     The twopoint
        /// </summary>
        public static readonly Font Twopoint = new _Twopoint();

        /// <summary>
        ///     The univers
        /// </summary>
        public static readonly Font Univers = new _Univers();

        /// <summary>
        ///     The usaflag
        /// </summary>
        public static readonly Font Usaflag = new _Usaflag();

        /// <summary>
        ///     The varsity
        /// </summary>
        public static readonly Font Varsity = new _Varsity();

        /// <summary>
        ///     The wavy
        /// </summary>
        public static readonly Font Wavy = new _Wavy();

        /// <summary>
        ///     The weird
        /// </summary>
        public static readonly Font Weird = new _Weird();

        /// <summary>
        ///     The wetletter
        /// </summary>
        public static readonly Font Wetletter = new _Wetletter();

        /// <summary>
        ///     The whimsy
        /// </summary>
        public static readonly Font Whimsy = new _Whimsy();

        /// <summary>
        ///     The wow
        /// </summary>
        public static readonly Font Wow = new _Wow();

        /// <summary>
        ///     The figlet font
        /// </summary>
        private FigletFont _figletFont;

        /// <summary>
        ///     Gets the figlet font.
        /// </summary>
        /// <value>The figlet font.</value>
        public virtual FigletFont FigletFont =>
            _figletFont ?? (_figletFont = FigletFont.Load($"figlet-fonts/{FileName}"));

        /// <summary>
        ///     Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public abstract string FileName { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        ///     Class _1row.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _1row : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "1row.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "1row";
        }

        /// <summary>
        ///     Class _3d.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _3d : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "3-d.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "3d";
        }

        /// <summary>
        ///     Class _3dDiagonal.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _3dDiagonal : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "3d_diagonal.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "3dDiagonal";
        }

        /// <summary>
        ///     Class _3x5.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _3x5 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "3x5.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "3x5";
        }

        /// <summary>
        ///     Class _4max.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _4max : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "4max.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "4max";
        }

        /// <summary>
        ///     Class _5lineoblique.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _5lineoblique : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "5lineoblique.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "5lineoblique";
        }

        /// <summary>
        ///     Class _Acrobatic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Acrobatic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "acrobatic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Acrobatic";
        }

        /// <summary>
        ///     Class _Alligator.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Alligator : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "alligator.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Alligator";
        }

        /// <summary>
        ///     Class _Alligator2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Alligator2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "alligator2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Alligator2";
        }

        /// <summary>
        ///     Class _Alligator3.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Alligator3 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "alligator3.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Alligator3";
        }

        /// <summary>
        ///     Class _Alpha.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Alpha : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "alpha.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Alpha";
        }

        /// <summary>
        ///     Class _Alphabet.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Alphabet : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "alphabet.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Alphabet";
        }

        /// <summary>
        ///     Class _Amc3line.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amc3line : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amc3line.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amc3line";
        }

        /// <summary>
        ///     Class _Amc3liv1.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amc3liv1 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amc3liv1.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amc3liv1";
        }

        /// <summary>
        ///     Class _Amcaaa01.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcaaa01 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcaaa01.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcaaa01";
        }

        /// <summary>
        ///     Class _Amcneko.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcneko : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcneko.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcneko";
        }

        /// <summary>
        ///     Class _Amcrazo2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcrazo2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcrazo2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcrazo2";
        }

        /// <summary>
        ///     Class _Amcrazor.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcrazor : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcrazor.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcrazor";
        }

        /// <summary>
        ///     Class _Amcslash.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcslash : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcslash.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcslash";
        }

        /// <summary>
        ///     Class _Amcslder.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcslder : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcslder.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcslder";
        }

        /// <summary>
        ///     Class _Amcthin.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcthin : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcthin.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcthin";
        }

        /// <summary>
        ///     Class _Amctubes.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amctubes : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amctubes.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amctubes";
        }

        /// <summary>
        ///     Class _Amcun1.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Amcun1 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "amcun1.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Amcun1";
        }

        /// <summary>
        ///     Class _Arrows.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Arrows : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "arrows.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Arrows";
        }

        /// <summary>
        ///     Class _AsciiNewRoman.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _AsciiNewRoman : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ascii_new_roman.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "AsciiNewRoman";
        }

        /// <summary>
        ///     Class _Avatar.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Avatar : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "avatar.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Avatar";
        }

        /// <summary>
        ///     Class _B1FF.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _B1FF : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "B1FF.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "B1FF";
        }

        /// <summary>
        ///     Class _Banner.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Banner : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "banner.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Banner";
        }

        /// <summary>
        ///     Class _Banner3.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Banner3 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "banner3.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Banner3";
        }

        /// <summary>
        ///     Class _Banner3D.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Banner3D : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "banner3-D.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Banner3D";
        }

        /// <summary>
        ///     Class _Banner4.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Banner4 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "banner4.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Banner4";
        }

        /// <summary>
        ///     Class _Barbwire.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Barbwire : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "barbwire.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Barbwire";
        }

        /// <summary>
        ///     Class _Basic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Basic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "basic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Basic";
        }

        /// <summary>
        ///     Class _Bear.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bear : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bear.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bear";
        }

        /// <summary>
        ///     Class _Bell.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bell : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bell.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bell";
        }

        /// <summary>
        ///     Class _Benjamin.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Benjamin : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "benjamin.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Benjamin";
        }

        /// <summary>
        ///     Class _Big.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Big : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "big.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Big";
        }

        /// <summary>
        ///     Class _Bigchief.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bigchief : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bigchief.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bigchief";
        }

        /// <summary>
        ///     Class _Bigfig.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bigfig : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bigfig.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bigfig";
        }

        /// <summary>
        ///     Class _Binary.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Binary : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "binary.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Binary";
        }

        /// <summary>
        ///     Class _Block.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Block : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "block.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Block";
        }

        /// <summary>
        ///     Class _Blocks.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Blocks : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "blocks.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Blocks";
        }

        /// <summary>
        ///     Class _Bolger.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bolger : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bolger.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bolger";
        }

        /// <summary>
        ///     Class _Braced.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Braced : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "braced.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Braced";
        }

        /// <summary>
        ///     Class _Bright.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bright : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bright.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bright";
        }

        /// <summary>
        ///     Class _Broadway.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Broadway : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "broadway.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Broadway";
        }

        /// <summary>
        ///     Class _BroadwayKb.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _BroadwayKb : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "broadway_kb.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "BroadwayKb";
        }

        /// <summary>
        ///     Class _Bubble.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bubble : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bubble.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bubble";
        }

        /// <summary>
        ///     Class _Bulbhead.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Bulbhead : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "bulbhead.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Bulbhead";
        }

        /// <summary>
        ///     Class _Calgphy2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Calgphy2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "calgphy2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Calgphy2";
        }

        /// <summary>
        ///     Class _Caligraphy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Caligraphy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "caligraphy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Caligraphy";
        }

        /// <summary>
        ///     Class _Cards.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cards : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cards.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cards";
        }

        /// <summary>
        ///     Class _Catwalk.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Catwalk : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "catwalk.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Catwalk";
        }

        /// <summary>
        ///     Class _Chiseled.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Chiseled : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "chiseled.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Chiseled";
        }

        /// <summary>
        ///     Class _Chunky.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Chunky : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "chunky.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Chunky";
        }

        /// <summary>
        ///     Class _Coinstak.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Coinstak : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "coinstak.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Coinstak";
        }

        /// <summary>
        ///     Class _Cola.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cola : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cola.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cola";
        }

        /// <summary>
        ///     Class _Colossal.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Colossal : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "colossal.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Colossal";
        }

        /// <summary>
        ///     Class _Computer.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Computer : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "computer.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Computer";
        }

        /// <summary>
        ///     Class _Contessa.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Contessa : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "contessa.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Contessa";
        }

        /// <summary>
        ///     Class _Contrast.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Contrast : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "contrast.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Contrast";
        }

        /// <summary>
        ///     Class _Cosmic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cosmic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cosmic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cosmic";
        }

        /// <summary>
        ///     Class _Cosmike.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cosmike : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cosmike.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cosmike";
        }

        /// <summary>
        ///     Class _Crawford.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Crawford : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "crawford.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Crawford";
        }

        /// <summary>
        ///     Class _Crazy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Crazy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "crazy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Crazy";
        }

        /// <summary>
        ///     Class _Cricket.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cricket : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cricket.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cricket";
        }

        /// <summary>
        ///     Class _Cyberlarge.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cyberlarge : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cyberlarge.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cyberlarge";
        }

        /// <summary>
        ///     Class _Cybermedium.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cybermedium : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cybermedium.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cybermedium";
        }

        /// <summary>
        ///     Class _Cybersmall.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cybersmall : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cybersmall.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cybersmall";
        }

        /// <summary>
        ///     Class _Cygnet.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Cygnet : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "cygnet.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Cygnet";
        }

        /// <summary>
        ///     Class _DANC4.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _DANC4 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "DANC4.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "DANC4";
        }

        /// <summary>
        ///     Class _Dancingfont.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Dancingfont : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "dancingfont.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Dancingfont";
        }

        /// <summary>
        ///     Class _Decimal.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Decimal : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "decimal.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Decimal";
        }

        /// <summary>
        ///     Class _Defleppard.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Defleppard : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "defleppard.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Defleppard";
        }

        /// <summary>
        ///     Class _Diamond.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Diamond : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "diamond.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Diamond";
        }

        /// <summary>
        ///     Class _Dietcola.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Dietcola : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "dietcola.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Dietcola";
        }

        /// <summary>
        ///     Class _Digital.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Digital : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "digital.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Digital";
        }

        /// <summary>
        ///     Class _Doh.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Doh : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "doh.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Doh";
        }

        /// <summary>
        ///     Class _Doom.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Doom : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "doom.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Doom";
        }

        /// <summary>
        ///     Class _Dosrebel.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Dosrebel : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "dosrebel.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Dosrebel";
        }

        /// <summary>
        ///     Class _Dotmatrix.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Dotmatrix : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "dotmatrix.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Dotmatrix";
        }

        /// <summary>
        ///     Class _Double.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Double : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "double.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Double";
        }

        /// <summary>
        ///     Class _Doubleshorts.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Doubleshorts : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "doubleshorts.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Doubleshorts";
        }

        /// <summary>
        ///     Class _Drpepper.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Drpepper : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "drpepper.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Drpepper";
        }

        /// <summary>
        ///     Class _Dwhistled.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Dwhistled : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "dwhistled.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Dwhistled";
        }

        /// <summary>
        ///     Class _Eftichess.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Eftichess : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "eftichess.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Eftichess";
        }

        /// <summary>
        ///     Class _Eftifont.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Eftifont : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "eftifont.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Eftifont";
        }

        /// <summary>
        ///     Class _Eftipiti.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Eftipiti : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "eftipiti.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Eftipiti";
        }

        /// <summary>
        ///     Class _Eftirobot.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Eftirobot : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "eftirobot.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Eftirobot";
        }

        /// <summary>
        ///     Class _Eftitalic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Eftitalic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "eftitalic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Eftitalic";
        }

        /// <summary>
        ///     Class _Eftiwall.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Eftiwall : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "eftiwall.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Eftiwall";
        }

        /// <summary>
        ///     Class _Eftiwater.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Eftiwater : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "eftiwater.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Eftiwater";
        }

        /// <summary>
        ///     Class _Epic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Epic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "epic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Epic";
        }

        /// <summary>
        ///     Class _Fender.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Fender : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "fender.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Fender";
        }

        /// <summary>
        ///     Class _Filter.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Filter : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "filter.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Filter";
        }

        /// <summary>
        ///     Class _FireFontk.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _FireFontk : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "fire_font-k.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "FireFontk";
        }

        /// <summary>
        ///     Class _FireFonts.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _FireFonts : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "fire_font-s.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "FireFonts";
        }

        /// <summary>
        ///     Class _Flipped.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Flipped : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "flipped.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Flipped";
        }

        /// <summary>
        ///     Class _Flowerpower.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Flowerpower : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "flowerpower.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Flowerpower";
        }

        /// <summary>
        ///     Class _Fourtops.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Fourtops : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "fourtops.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Fourtops";
        }

        /// <summary>
        ///     Class _Fraktur.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Fraktur : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "fraktur.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Fraktur";
        }

        /// <summary>
        ///     Class _Funface.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Funface : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "funface.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Funface";
        }

        /// <summary>
        ///     Class _Funfaces.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Funfaces : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "funfaces.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Funfaces";
        }

        /// <summary>
        ///     Class _Fuzzy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Fuzzy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "fuzzy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Fuzzy";
        }

        /// <summary>
        ///     Class _Georgi16.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Georgi16 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "georgi16.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Georgi16";
        }

        /// <summary>
        ///     Class _Georgia11.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Georgia11 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "Georgia11.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Georgia11";
        }

        /// <summary>
        ///     Class _Ghost.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Ghost : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ghost.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Ghost";
        }

        /// <summary>
        ///     Class _Ghoulish.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Ghoulish : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ghoulish.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Ghoulish";
        }

        /// <summary>
        ///     Class _Glenyn.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Glenyn : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "glenyn.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Glenyn";
        }

        /// <summary>
        ///     Class _Goofy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Goofy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "goofy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Goofy";
        }

        /// <summary>
        ///     Class _Gothic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Gothic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "gothic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Gothic";
        }

        /// <summary>
        ///     Class _Graceful.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Graceful : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "graceful.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Graceful";
        }

        /// <summary>
        ///     Class _Gradient.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Gradient : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "gradient.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Gradient";
        }

        /// <summary>
        ///     Class _Graffiti.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Graffiti : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "graffiti.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Graffiti";
        }

        /// <summary>
        ///     Class _Greek.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Greek : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "greek.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Greek";
        }

        /// <summary>
        ///     Class _HeartLeft.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _HeartLeft : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "heart_left.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "HeartLeft";
        }

        /// <summary>
        ///     Class _HeartRight.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _HeartRight : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "heart_right.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "HeartRight";
        }

        /// <summary>
        ///     Class _Henry3d.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Henry3d : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "henry3d.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Henry3d";
        }

        /// <summary>
        ///     Class _Hex.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Hex : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "hex.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Hex";
        }

        /// <summary>
        ///     Class _Hieroglyphs.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Hieroglyphs : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "hieroglyphs.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Hieroglyphs";
        }

        /// <summary>
        ///     Class _Hollywood.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Hollywood : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "hollywood.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Hollywood";
        }

        /// <summary>
        ///     Class _Horizontalleft.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Horizontalleft : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "horizontalleft.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Horizontalleft";
        }

        /// <summary>
        ///     Class _Horizontalright.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Horizontalright : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "horizontalright.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Horizontalright";
        }

        /// <summary>
        ///     Class _ICL1900.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _ICL1900 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ICL-1900.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "ICL1900";
        }

        /// <summary>
        ///     Class _Impossible.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Impossible : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "impossible.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Impossible";
        }

        /// <summary>
        ///     Class _Invita.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Invita : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "invita.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Invita";
        }

        /// <summary>
        ///     Class _Isometric1.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Isometric1 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "isometric1.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Isometric1";
        }

        /// <summary>
        ///     Class _Isometric2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Isometric2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "isometric2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Isometric2";
        }

        /// <summary>
        ///     Class _Isometric3.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Isometric3 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "isometric3.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Isometric3";
        }

        /// <summary>
        ///     Class _Isometric4.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Isometric4 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "isometric4.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Isometric4";
        }

        /// <summary>
        ///     Class _Italic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Italic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "italic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Italic";
        }

        /// <summary>
        ///     Class _Ivrit.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Ivrit : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ivrit.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Ivrit";
        }

        /// <summary>
        ///     Class _Jacky.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Jacky : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "jacky.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Jacky";
        }

        /// <summary>
        ///     Class _Jazmine.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Jazmine : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "jazmine.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Jazmine";
        }

        /// <summary>
        ///     Class _Jerusalem.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Jerusalem : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "jerusalem.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Jerusalem";
        }

        /// <summary>
        ///     Class _Katakana.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Katakana : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "katakana.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Katakana";
        }

        /// <summary>
        ///     Class _Kban.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Kban : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "kban.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Kban";
        }

        /// <summary>
        ///     Class _Keyboard.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Keyboard : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "keyboard.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Keyboard";
        }

        /// <summary>
        ///     Class _Knob.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Knob : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "knob.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Knob";
        }

        /// <summary>
        ///     Class _Konto.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Konto : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "konto.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Konto";
        }

        /// <summary>
        ///     Class _Kontoslant.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Kontoslant : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "kontoslant.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Kontoslant";
        }

        /// <summary>
        ///     Class _Larry3d.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Larry3d : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "larry3d.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Larry3d";
        }

        /// <summary>
        ///     Class _Lcd.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Lcd : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "lcd.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Lcd";
        }

        /// <summary>
        ///     Class _Lean.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Lean : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "lean.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Lean";
        }

        /// <summary>
        ///     Class _Letters.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Letters : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "letters.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Letters";
        }

        /// <summary>
        ///     Class _Lildevil.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Lildevil : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "lildevil.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Lildevil";
        }

        /// <summary>
        ///     Class _Lineblocks.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Lineblocks : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "lineblocks.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Lineblocks";
        }

        /// <summary>
        ///     Class _Linux.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Linux : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "linux.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Linux";
        }

        /// <summary>
        ///     Class _Lockergnome.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Lockergnome : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "lockergnome.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Lockergnome";
        }

        /// <summary>
        ///     Class _Madrid.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Madrid : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "madrid.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Madrid";
        }

        /// <summary>
        ///     Class _Marquee.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Marquee : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "marquee.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Marquee";
        }

        /// <summary>
        ///     Class _Maxfour.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Maxfour : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "maxfour.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Maxfour";
        }

        /// <summary>
        ///     Class _Merlin1.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Merlin1 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "merlin1.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Merlin1";
        }

        /// <summary>
        ///     Class _Merlin2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Merlin2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "merlin2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Merlin2";
        }

        /// <summary>
        ///     Class _Mike.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Mike : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "mike.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Mike";
        }

        /// <summary>
        ///     Class _Mini.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Mini : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "mini.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Mini";
        }

        /// <summary>
        ///     Class _Mirror.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Mirror : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "mirror.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Mirror";
        }

        /// <summary>
        ///     Class _Mnemonic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Mnemonic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "mnemonic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Mnemonic";
        }

        /// <summary>
        ///     Class _Modular.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Modular : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "modular.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Modular";
        }

        /// <summary>
        ///     Class _Morse.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Morse : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "morse.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Morse";
        }

        /// <summary>
        ///     Class _Morse2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Morse2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "morse2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Morse2";
        }

        /// <summary>
        ///     Class _Moscow.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Moscow : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "moscow.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Moscow";
        }

        /// <summary>
        ///     Class _Mshebrew210.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Mshebrew210 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "mshebrew210.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Mshebrew210";
        }

        /// <summary>
        ///     Class _Muzzle.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Muzzle : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "muzzle.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Muzzle";
        }

        /// <summary>
        ///     Class _Nancyj.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Nancyj : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "nancyj.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Nancyj";
        }

        /// <summary>
        ///     Class _Nancyjfancy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Nancyjfancy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "nancyj-fancy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Nancyjfancy";
        }

        /// <summary>
        ///     Class _Nancyjimproved.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Nancyjimproved : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "nancyj-improved.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Nancyjimproved";
        }

        /// <summary>
        ///     Class _Nancyjunderlined.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Nancyjunderlined : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "nancyj-underlined.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Nancyjunderlined";
        }

        /// <summary>
        ///     Class _Nipples.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Nipples : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "nipples.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Nipples";
        }

        /// <summary>
        ///     Class _Nscript.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Nscript : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "nscript.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Nscript";
        }

        /// <summary>
        ///     Class _Ntgreek.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Ntgreek : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ntgreek.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Ntgreek";
        }

        /// <summary>
        ///     Class _Nvscript.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Nvscript : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "nvscript.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Nvscript";
        }

        /// <summary>
        ///     Class _O8.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _O8 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "o8.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "O8";
        }

        /// <summary>
        ///     Class _Octal.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Octal : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "octal.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Octal";
        }

        /// <summary>
        ///     Class _Ogre.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Ogre : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ogre.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Ogre";
        }

        /// <summary>
        ///     Class _Oldbanner.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Oldbanner : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "oldbanner.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Oldbanner";
        }

        /// <summary>
        ///     Class _Os2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Os2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "os2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Os2";
        }

        /// <summary>
        ///     Class _Pawp.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Pawp : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "pawp.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Pawp";
        }

        /// <summary>
        ///     Class _Peaks.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Peaks : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "peaks.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Peaks";
        }

        /// <summary>
        ///     Class _Peaksslant.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Peaksslant : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "peaksslant.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Peaksslant";
        }

        /// <summary>
        ///     Class _Pebbles.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Pebbles : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "pebbles.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Pebbles";
        }

        /// <summary>
        ///     Class _Pepper.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Pepper : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "pepper.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Pepper";
        }

        /// <summary>
        ///     Class _Poison.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Poison : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "poison.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Poison";
        }

        /// <summary>
        ///     Class _Puffy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Puffy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "puffy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Puffy";
        }

        /// <summary>
        ///     Class _Puzzle.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Puzzle : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "puzzle.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Puzzle";
        }

        /// <summary>
        ///     Class _Pyramid.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Pyramid : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "pyramid.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Pyramid";
        }

        /// <summary>
        ///     Class _Rammstein.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Rammstein : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "rammstein.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Rammstein";
        }

        /// <summary>
        ///     Class _Rectangles.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Rectangles : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "rectangles.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Rectangles";
        }

        /// <summary>
        ///     Class _RedPhoenix.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _RedPhoenix : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "red_phoenix.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "RedPhoenix";
        }

        /// <summary>
        ///     Class _Relief.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Relief : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "relief.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Relief";
        }

        /// <summary>
        ///     Class _Relief2.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Relief2 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "relief2.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Relief2";
        }

        /// <summary>
        ///     Class _Reverse.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Reverse : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "reverse.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Reverse";
        }

        /// <summary>
        ///     Class _Roman.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Roman : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "roman.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Roman";
        }

        /// <summary>
        ///     Class _Rot13.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Rot13 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "rot13.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Rot13";
        }

        /// <summary>
        ///     Class _Rotated.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Rotated : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "rotated.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Rotated";
        }

        /// <summary>
        ///     Class _Rounded.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Rounded : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "rounded.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Rounded";
        }

        /// <summary>
        ///     Class _Rowancap.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Rowancap : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "rowancap.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Rowancap";
        }

        /// <summary>
        ///     Class _Rozzo.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Rozzo : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "rozzo.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Rozzo";
        }

        /// <summary>
        ///     Class _Runic.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Runic : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "runic.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Runic";
        }

        /// <summary>
        ///     Class _Runyc.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Runyc : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "runyc.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Runyc";
        }

        /// <summary>
        ///     Class _Santaclara.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Santaclara : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "santaclara.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Santaclara";
        }

        /// <summary>
        ///     Class _Sblood.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Sblood : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "sblood.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Sblood";
        }

        /// <summary>
        ///     Class _Script.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Script : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "script.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Script";
        }

        /// <summary>
        ///     Class _Serifcap.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Serifcap : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "serifcap.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Serifcap";
        }

        /// <summary>
        ///     Class _Shadow.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Shadow : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "shadow.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Shadow";
        }

        /// <summary>
        ///     Class _Shimrod.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Shimrod : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "shimrod.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Shimrod";
        }

        /// <summary>
        ///     Class _Short.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Short : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "short.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Short";
        }

        /// <summary>
        ///     Class _Slant.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Slant : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "slant.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Slant";
        }

        /// <summary>
        ///     Class _Slide.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Slide : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "slide.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Slide";
        }

        /// <summary>
        ///     Class _Slscript.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Slscript : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "slscript.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Slscript";
        }

        /// <summary>
        ///     Class _Small.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Small : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "small.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Small";
        }

        /// <summary>
        ///     Class _Smallcaps.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smallcaps : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smallcaps.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smallcaps";
        }

        /// <summary>
        ///     Class _Smisome1.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smisome1 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smisome1.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smisome1";
        }

        /// <summary>
        ///     Class _Smkeyboard.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smkeyboard : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smkeyboard.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smkeyboard";
        }

        /// <summary>
        ///     Class _Smpoison.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smpoison : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smpoison.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smpoison";
        }

        /// <summary>
        ///     Class _Smscript.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smscript : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smscript.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smscript";
        }

        /// <summary>
        ///     Class _Smshadow.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smshadow : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smshadow.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smshadow";
        }

        /// <summary>
        ///     Class _Smslant.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smslant : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smslant.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smslant";
        }

        /// <summary>
        ///     Class _Smtengwar.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Smtengwar : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "smtengwar.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Smtengwar";
        }

        /// <summary>
        ///     Class _Soft.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Soft : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "soft.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Soft";
        }

        /// <summary>
        ///     Class _Speed.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Speed : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "speed.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Speed";
        }

        /// <summary>
        ///     Class _Spliff.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Spliff : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "spliff.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Spliff";
        }

        /// <summary>
        ///     Class _Srelief.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Srelief : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "s-relief.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Srelief";
        }

        /// <summary>
        ///     Class _Stacey.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Stacey : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "stacey.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Stacey";
        }

        /// <summary>
        ///     Class _Stampate.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Stampate : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "stampate.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Stampate";
        }

        /// <summary>
        ///     Class _Stampatello.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Stampatello : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "stampatello.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Stampatello";
        }

        /// <summary>
        ///     Class _Standard.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Standard : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "standard.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Standard";
        }

        /// <summary>
        ///     Class _Starstrips.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Starstrips : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "starstrips.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Starstrips";
        }

        /// <summary>
        ///     Class _Starwars.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Starwars : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "starwars.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Starwars";
        }

        /// <summary>
        ///     Class _Stellar.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Stellar : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "stellar.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Stellar";
        }

        /// <summary>
        ///     Class _Stforek.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Stforek : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "stforek.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Stforek";
        }

        /// <summary>
        ///     Class _Stop.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Stop : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "stop.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Stop";
        }

        /// <summary>
        ///     Class _Straight.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Straight : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "straight.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Straight";
        }

        /// <summary>
        ///     Class _Subzero.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Subzero : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "sub-zero.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Subzero";
        }

        /// <summary>
        ///     Class _Swampland.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Swampland : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "swampland.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Swampland";
        }

        /// <summary>
        ///     Class _Swan.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Swan : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "swan.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Swan";
        }

        /// <summary>
        ///     Class _Sweet.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Sweet : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "sweet.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Sweet";
        }

        /// <summary>
        ///     Class _Tanja.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Tanja : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "tanja.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Tanja";
        }

        /// <summary>
        ///     Class _Tengwar.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Tengwar : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "tengwar.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Tengwar";
        }

        /// <summary>
        ///     Class _Term.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Term : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "term.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Term";
        }

        /// <summary>
        ///     Class _Test1.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Test1 : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "test1.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Test1";
        }

        /// <summary>
        ///     Class _Thick.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Thick : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "thick.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Thick";
        }

        /// <summary>
        ///     Class _Thin.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Thin : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "thin.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Thin";
        }

        /// <summary>
        ///     Class _Threepoint.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Threepoint : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "threepoint.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Threepoint";
        }

        /// <summary>
        ///     Class _Ticks.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Ticks : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ticks.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Ticks";
        }

        /// <summary>
        ///     Class _Ticksslant.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Ticksslant : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "ticksslant.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Ticksslant";
        }

        /// <summary>
        ///     Class _Tiles.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Tiles : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "tiles.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Tiles";
        }

        /// <summary>
        ///     Class _Tinkertoy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Tinkertoy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "tinker-toy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Tinkertoy";
        }

        /// <summary>
        ///     Class _Tombstone.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Tombstone : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "tombstone.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Tombstone";
        }

        /// <summary>
        ///     Class _Train.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Train : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "train.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Train";
        }

        /// <summary>
        ///     Class _Trek.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Trek : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "trek.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Trek";
        }

        /// <summary>
        ///     Class _Tsalagi.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Tsalagi : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "tsalagi.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Tsalagi";
        }

        /// <summary>
        ///     Class _Tubular.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Tubular : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "tubular.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Tubular";
        }

        /// <summary>
        ///     Class _Twisted.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Twisted : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "twisted.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Twisted";
        }

        /// <summary>
        ///     Class _Twopoint.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Twopoint : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "twopoint.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Twopoint";
        }

        /// <summary>
        ///     Class _Univers.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Univers : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "univers.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Univers";
        }

        /// <summary>
        ///     Class _Usaflag.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Usaflag : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "usaflag.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Usaflag";
        }

        /// <summary>
        ///     Class _Varsity.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Varsity : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "varsity.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Varsity";
        }

        /// <summary>
        ///     Class _Wavy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Wavy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "wavy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Wavy";
        }

        /// <summary>
        ///     Class _Weird.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Weird : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "weird.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Weird";
        }

        /// <summary>
        ///     Class _Wetletter.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Wetletter : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "wetletter.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Wetletter";
        }

        /// <summary>
        ///     Class _Whimsy.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Whimsy : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "whimsy.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Whimsy";
        }

        /// <summary>
        ///     Class _Wow.
        /// </summary>
        /// <seealso cref="ArgParser.Styles.Alba.Font" />
        private class _Wow : Font
        {
            /// <summary>
            ///     Gets the name of the file.
            /// </summary>
            /// <value>The name of the file.</value>
            public override string FileName { get; } = "wow.flf";

            /// <summary>
            ///     Gets the name.
            /// </summary>
            /// <value>The name.</value>
            public override string Name { get; } = "Wow";
        }
    }
}