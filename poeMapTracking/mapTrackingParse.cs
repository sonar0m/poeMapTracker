using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Runtime.Serialization;
using System.Linq;
using MovablePython;

namespace poeMapTracking
{

    [Flags]
    public enum State { waiting = 0, gettingMap = 1, running = 2, stopped = 3 }
    public class MapTracker : Object
    {
        #region Static
        [STAThread]
        static void Main(string[] args)
        {
            #region test maps
            runTest();
            #endregion
            string clipboard = string.Empty;
            MapTracker mt = new MapTracker();
            bool breakout = false;
            mt.state = State.waiting;
            do
            {
                clipboard = getClipboard();
                switch (mt.state)
                {
                    case State.gettingMap://complete setting map over ride if repeated??
                        mt.runningMap = new Map(clipboard);
                        Clipboard.Clear();// clear the clipboard
                        mt.state = State.running;
                        break;
                    case State.running://needs to be idented?? clear clipboard
                        Map map = new Map(clipboard);
                        if (!mt.mapsOutput.Exists(e => e.fullName == map.fullName))
                        {//add bypass?
                            mt.mapsOutput.Add(map);
                            Clipboard.Clear();
                        }
                        else
                        {
                            System.Console.Out.WriteLine("repeat map");
                            Clipboard.Clear();
                        }
                        break;
                    case State.waiting://waiting to start exaimin info
                        if (Map.isMap(clipboard))
                            mt.state = State.gettingMap;
                        break;
                    case State.stopped://write data go to waiting after wards
                        break;
                    default:
                        break;
                }
            } while (!breakout);
            Console.ReadKey();
        }
        [STAThread]
        public static string getClipboard()
        {
            try
            {
                System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);
            }
            catch
            {
                System.Console.Out.WriteLine("OUCH!");
            }
            System.Threading.Thread.CurrentThread.TrySetApartmentState(System.Threading.ApartmentState.STA);
            bool breakout = false;
            string s = string.Empty;
            while (!breakout)
            {
                //ConsoleKeyInfo key = Console.ReadKey(true);
                //Console.WriteLine("  Key pressed: {0}\n", key.Key);
                if (Clipboard.GetText() != s)
                {
                    s = Clipboard.GetText();
                    if (Map.isMap(s))
                    {
                        Console.Out.WriteLine("found!");
                        //Console.Out.Write(s);
                        breakout = true;
                    }//if clipboard is POE
                }//if clipboard is new
            }//while loop
            return s;
        }//end function
        #region tests
        private static void runTest()
        { //Todo: log' and enable/disable 
            #region setup
            string[] maps = {@"Rotting Secrets
Tropical Island Map
--------
Map Tier: 1
Item Quantity: +55% (augmented)
Item Rarity: +28% (augmented)
--------
Item Level: 68
--------
20% more Magic Monsters
Monsters fire 2 additional Projectiles
+40% Monster Lightning Resistance
Monsters are Immune to Curses
Magic Monster Packs each have a Bloodline Mod
--------
Travel to this Map by using it in the Eternal Laboratory or a personal Map Device. Maps can only be used once."
                            ,@"Rarity: Magic
Overlord's Crypt Map of Smothering
--------
Map Tier: 1
Item Quantity: +30% (augmented)
Item Rarity: +20% (augmented)
Monster Pack Size: +15% (augmented)
--------
Item Level: 68
--------
Players Recover Life, Mana and Energy Shield 20% slower
Unique Boss deals 25% increased Damage
Unique Boss has 20% increased Attack and Cast Speed
--------
Travel to this Map by using it in the Eternal Laboratory or a personal Map Device. Maps can only be used once."
                            ,@"Rarity: Normal
Superior Grotto Map
--------
Map Tier: 1
Item Quantity: +19% (augmented)
Quality: +19% (augmented)
--------
Item Level: 69
--------
Travel to this Map by using it in the Eternal Laboratory or a personal Map Device. Maps can only be used once.
"
                            };
            #endregion
            foreach (string map in maps)
            {
                poeMapTracking.Map m = new Map(map);
            }
        }//end runTest
        #endregion
        #endregion
        //sunsetted for futchure options
        //private string flagName;
        //private void setFlag(string item)
        //{
        //    Item i = new Item(item);
        //    this.flagName = i.fullName;
        //}
        public List<Map> mapsOutput;
        public Map runningMap;
        public State state = State.waiting;
        public MapTracker()
        {
            mapsOutput = new List<Map>();
            runningMap = new Map();
            state = State.waiting;
            //this.flagName = string.Empty;
        }
        //sunseted for futchure
        //private bool trigerTest(string item)
        //{
        //    Item i = new Item(item);
        //    if (this.flagName == i.fullName)
        //        return true;
        //    return false;
        //}//end main
        public override string ToString()
        {
            string ret = ConfigurationManager.AppSettings["outputOrder"];
            int maxTier = 15;
            int.TryParse(ConfigurationManager.AppSettings["maxTier"], out maxTier);
            #region get count
            int[] count = getTierCount();
            #endregion
            string[] toReplace = ConfigurationManager.AppSettings["outputOrder"].Split(new string[] { ConfigurationManager.AppSettings["csv"] }, StringSplitOptions.RemoveEmptyEntries);
            #region replace
            ret = ret.Replace("{tier}", this.runningMap.mapTier.ToString());
            ret = ret.Replace("{itemquantity}", this.runningMap.itemQuantity.ToString());
            ret = ret.Replace("{packsize}", this.runningMap.packSize.ToString());
            ret = ret.Replace("{itemrarity}", this.runningMap.itemRarity.ToString());
            ret = ret.Replace("{quality}", this.runningMap.quality.ToString());
            ret = ret.Replace("{0}", count[this.runningMap.mapTier-1].ToString());
            #region replace numbers
            int startLooking = ret.IndexOf("{+");
            while (startLooking != -1)
            {
                string replacing = ret.Substring(startLooking, ret.IndexOf("}", startLooking) + 1 - startLooking);
                int tier = 0;
                int.TryParse(replacing.Substring(2).Substring(0, replacing.Length - 3), out tier);
                tier = this.runningMap.mapTier + tier;
                if (1 < tier && tier < maxTier)
                    ret = ret.Replace(replacing, count[tier - 1].ToString());
                else
                    ret = ret.Replace(replacing, 0.ToString());
                startLooking = ret.IndexOf("{+");
            }
            startLooking = ret.IndexOf("{-");
            while (startLooking != -1)
            {
                string replacing = ret.Substring(startLooking, ret.IndexOf("}", startLooking) + 1 - startLooking);
                int tier = 0;
                int.TryParse(replacing.Substring(2).Substring(0, replacing.Length - 3), out tier);//2 from {+ 1 from off by one error
                tier = this.runningMap.mapTier - tier;
                if (1 < tier && tier < maxTier)
                    ret = ret.Replace(replacing, count[tier - 1].ToString());
                else
                    ret = ret.Replace(replacing, 0.ToString());
                startLooking = ret.IndexOf("{-");
            }
            for (int i = 0; i < maxTier; i++)
            {
                ret = ret.Replace("{" + i + 1 + "}", count[i].ToString());
            }
            #endregion
            #endregion
            return ret;
        }
        public void clearTier(int tier)
        {
             this.mapsOutput.RemoveAll(t => t.mapTier ==tier);
        }
        public int[] getTierCount()
        {
            int maxTier = 15;
            int.TryParse(ConfigurationManager.AppSettings["maxTier"], out maxTier);
            int[] count = new int[maxTier];
            for (int i = 0; i < maxTier; i++)
            {
                count[i] = 0;//init
            }
            foreach (Map m in this.mapsOutput)
            {
                count[m.mapTier - 1]++;
            }
            return count;
        }
    }//end function class
    public class Item : Object /*, ISerializable*/
    {
        protected string _fullText;
        public string fullText { get { return _fullText; } set { _fullText = this.parse(value); } }
        protected string _rarity, _baseName, _fullName;
        public string rarity { get { return _rarity; } }
        public string baseName { get { return _baseName; } }
        public string fullName { get { return _fullName; } }
        protected int itemLevel;
        protected List<Affix> _affix;
        public List<Affix> affix { get { return _affix; } }
        public Item()
        {
        }
        public Item(string full)
        {
            this.parse(full);
        }
        public static bool isItem(string full)
        {
            return full.Contains("Item Level: ");
        }
        public virtual string parse(string full)
        {
            string[] info = full.Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries);
            if (info[0].StartsWith("Rarity: "))
            {
                this._rarity = info[0].Substring("Rarity: ".Length, info[0].IndexOf(Environment.NewLine) - "Rarity: ".Length);
                this._fullName = info[0].Substring(info[0].IndexOf(Environment.NewLine));
            }
            else
            {

                this._fullName = info[0];
            }
            switch (rarity)
            {
                case "Currency":
                    break;
            }
            return full;
        }//to be filled out by sub class

        //public Item(SerializationInfo info, StreamingContext context)
        //{
        //    this.parse(info.GetString("fullText"));
        //}
        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("fullText", _fullText);
        //    foreach (Affix a in affix)
        //    {
        //        info.AddValue("affix", (Object)a,  affix.GetType());
        //    }
        //}
    }//end item class
    public class Map : Item
    {
        #region storage for map info
        protected int _mapTier = 0;
        public int mapTier { get { return _mapTier; } }
        protected int _itemQuantity = 0;
        public int itemQuantity { get { return _itemQuantity; } }
        protected int _packSize = 0;
        public int packSize { get { return _packSize; } }
        protected int _ItemRarity = 0;
        public int itemRarity { get { return _ItemRarity; } }
        protected int _quality = 0;
        public int quality { get { return _quality; } }
        private static string validReplace = "{tier},{itemquantity},{packsize},{itemrarity},{quality}";
        public static string defaultOrder = "{tier},{itemquantity},{itemrarity},{packsize},{quality}";
        #endregion
        public string output
        {
            get
            {

                string ret = Map.defaultOrder;
                if (ConfigurationManager.AppSettings["outputOrder"] != string.Empty)
                    ret = ConfigurationManager.AppSettings["outputOrder"];
                ret = ret.Replace("{tier}", "" + mapTier);
                ret = ret.Replace("{itemquantity}", "" + itemQuantity);
                ret = ret.Replace("{packsize}", "" + packSize);
                ret = ret.Replace("{itemrarity}", "" + itemRarity);
                ret = ret.Replace("{quality}", "" + quality);
                return ret;
            }
        }

        public Map()
        {
        }
        public Map(string map)
            : base(map)
        {
            if (!Map.isMap(map))
            {
                throw new Exception("Not a map");
            }
            this.parse(map);
        }
        public Map(SerializationInfo info, StreamingContext context)
        {
            this.parse(info.GetString("fullText"));
        }
        public static bool isMap(string full)
        {
            return full.Contains("Map Tier: ");
        }
        public override string parse(string full)
        {
            this._rarity = "Normal";
            this._fullText = full;
            string[] info = full.Split(new string[] { "--------" }, StringSplitOptions.RemoveEmptyEntries);
            if (info.Length >= 4)
            {//test if rarity is there
                if (info[0].StartsWith("Rarity: "))
                {
                    this._rarity = info[0].Substring("Rarity: ".Length, info[0].IndexOf(Environment.NewLine) - "Rarity: ".Length);
                    this._fullName = info[0].Substring(info[0].IndexOf(Environment.NewLine));
                }
                else
                {

                    this._fullName = info[0];
                }
                if (info[1].Contains("Map Tier: "))
                {
                    this._mapTier = 0;
                    int start = info[1].IndexOf("Map Tier: ") + "Map Tier: ".Length;
                    int.TryParse(info[1].Substring(start, info[1].IndexOf(Environment.NewLine, start) - start), out this._mapTier);
                }
                #region efficting mods
                if (info[1].Contains("Item Quantity: "))
                {
                    this._itemQuantity = 0;
                    int start = info[1].IndexOf("Item Quantity: ") + "Item Quantity: ".Length;
                    string toParse = info[1].Substring(start, info[1].IndexOf(Environment.NewLine, start) - start);
                    toParse = toParse.Substring(1, toParse.IndexOf("%") - 1);
                    int.TryParse(toParse, out this._itemQuantity);
                }//end quantity
                if (info[1].Contains("Item Rarity: "))
                {
                    this._ItemRarity = 0;
                    int start = info[1].IndexOf("Item Rarity: ") + "Item Rarity: ".Length;
                    string toParse = info[1].Substring(start, info[1].IndexOf(Environment.NewLine, start) - start);
                    toParse = toParse.Substring(1, toParse.IndexOf("%") - 1);
                    int.TryParse(toParse, out this._ItemRarity);
                }//end rarity
                if (info[1].Contains(Environment.NewLine + "Quality: "))
                {
                    this._quality = 0;
                    int start = info[1].IndexOf(Environment.NewLine + "Quality: ") + (Environment.NewLine + "Quality: ").Length;
                    string toParse = info[1].Substring(start, info[1].IndexOf(Environment.NewLine, start) - start);
                    toParse = toParse.Substring(1, toParse.IndexOf("%") - 1);
                    int.TryParse(toParse, out this._quality);
                }//end quality
                if (info[1].Contains("Monster Pack Size: "))
                {
                    this._packSize = 0;
                    int start = info[1].IndexOf("Monster Pack Size: ") + "Monster Pack Size: ".Length;
                    string toParse = info[1].Substring(start, info[1].IndexOf(Environment.NewLine, start) - start);
                    toParse = toParse.Substring(1, toParse.IndexOf("%") - 1);
                    int.TryParse(toParse, out this._packSize);
                }//end pack size
                #endregion
                this._fullName = this._fullName.Replace(Environment.NewLine, " ");
                this._baseName = this.fullName;//tbd
                if (this.baseName.Contains(Environment.NewLine))
                    this._baseName = this._baseName.Replace(Environment.NewLine, " ");
                this._baseName = this._baseName.Replace("Superior ", "");
                //Todo: need to find pre affixes in name
                this._baseName = this._baseName.Substring(0, this._baseName.IndexOf("Map") + "Map".Length);
                if (!int.TryParse(info[2].Substring(info[2].IndexOf("Item Level: ") + "Item Level: ".Length), out this.itemLevel))
                {
                    this.itemLevel = 0;
                }
                this._affix = new List<Affix>();
                if (info[3] != Environment.NewLine + "Travel to this Map by using it in the Eternal Laboratory or a personal Map Device. Maps can only be used once." + Environment.NewLine)
                    foreach (string aff in info[3].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        //determan pre and post affixes
                        this._affix.Add(new Affix(aff));
                    }//end of affix loop
            }//end test for length
            //var alpha = from a in affix
            //        where a.isPre 
            //        select a;
            return full;
        }//end parse
        public override string ToString()
        {
            return this._fullText;
        }
    }
    public class Affix : Object
    {
        public bool isPre = false, isPost = false;
        public string fullTex, lable = string.Empty;
        public int number = -1;
        public Affix(string f = "", bool pre = false, bool post = false)
        {
            fullTex = f;
            isPre = pre;
            isPost = post;
        }
        public override string ToString()
        {
            return fullTex;
        }
    }//end Affix class
}//end namespace
