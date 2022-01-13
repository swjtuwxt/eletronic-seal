using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;    //使用外部Win32 API



namespace ele_seal
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        Seal seal;

        [DllImport("gdi32", EntryPoint = "CreateFont")]
     
        //[DllImport("coredll.dll", EntryPoint = "CreateFontIndirect")]
        public static extern IntPtr CreateFontIndirect(
        int nHeight,                //字符逻辑高度
        int nWidth,                 //逻辑单位指定字体字符的平均宽度
        int nEscapement,            //以十分之一度为单位指定每一行文本输出时相对于页面底端的角度
        int nOrientation,           //以十分之一度为单位指定字符基线相对于页面底端的角度
        int fnWeight,               //字体重量,0~1000,正常为400，粗体为700。0则为默认的字体重量
        uint fdwItalic,             //lfItalic为TRUE时使用斜体
        uint fdwUnderline,          //lfUnderline为TRUE时给字体添加下划线
        uint fdwStrikeOut,          //当lfStrikeOut为TRUE时给字体添加删除线
        uint fdwCharSet,            //指定字符集。可以使用下面的预定义值：ANSI_CHARSET、OEM_CHARSET、SYMBOL_CHARSET、UNICODE_CHARSET，其中OEM字符集是与操作系统相关的
        uint fdwOutputPrecision,    //指定输出精度,OUT_CHARACTER_PRECIS、OUT_DEFAULT_PRECIS、OUT_STRING_PRECIS、OUT_STROKE_PRECIS
        uint fdwClipPrecision,      //剪辑精度。定义当字符超过剪辑区时对字符的剪辑方式，CLIP_CHARACTER_PRECIS、CLIP_DEFAULT_PRECIS、CLIP_STROKE_PRECIS
        uint fdwQuality,            //输出质量。定义图形设备接口在匹配逻辑字体属性到实际的物理字体的使用的方式，DEFAULT_QUALITY (默认质量)、DRAFT_QUALITY (草稿质量)、PROOF_QUALITY (正稿质量)
        uint fdwPitchAndFamily,     //字体的字符间距和族
        string lpszFace);           //字体名

        const int ANSI_CHARSET = 0;
        const int GB2312_CHARSET = 134;
        const int OUT_DEFAULT_PRECIS = 0;
        const int CLIP_DEFAULT_PRECIS = 0;
        const int DEFAULT_QUALITY = 0;
        const int DEFAULT_PITCH = 0;
               
        public Form1()
        {
            InitializeComponent();
            ShapeBox.SelectedIndex = 0;
            CenterBox.SelectedIndex = 0;
            FontBox.SelectedIndex = 0;         

            Draws();
        }
        
        public void Draws()
        {
            bitmap = new Bitmap(showPictureBox.Width, showPictureBox.Height);
            seal = new Seal(bitmap);

            
            string fs= FontBox.Text.ToString();
            IntPtr A = CreateFontIndirect(                                      
            0,                          //字符逻辑高度，默认为0
            0,
            0,
            0,
            700,                        //粗细100
            0,
            0,
            0,
            ANSI_CHARSET,               //字符集，ANSI_CHARSET
            OUT_DEFAULT_PRECIS,
            CLIP_DEFAULT_PRECIS,
            DEFAULT_QUALITY,
            DEFAULT_PITCH,
            fs);
            Font myFont = Font.FromHfont(A);


            Graphics g = Graphics.FromImage(bitmap);
           

            if (ShapeBox.SelectedIndex == 1)//绘制矩形印章
            {
                int width = Convert.ToInt32(BreathUpDown.Text.ToString());
                int high = Convert.ToInt32(HeightUpDown.Text.ToString());
                int Lwidth = Convert.ToInt32(LineBreadthUpDown.Text.ToString());
                

                String str1 = inputText2.Text.ToString();
                int Fsize = Convert.ToInt32(FontsizeBox.Text.ToString());
                String Font = FontBox.Text.ToString();

                int LSpace = Convert.ToInt32(LetterSpace.Text.ToString());
                seal.CreateFrame_1(width, high, Lwidth);


                //我的水印
                //g.DrawString("支付宝账号：\n13568301905", myFont, new SolidBrush(Color.Black), bitmap.Width / 2 - 50, bitmap.Height / 2);


                bool bold= checkBox1.Checked ? true : false;
                seal.CreateText_1(str1,Fsize, Font, LSpace,bold);   
                //seal.CreateText_1(str1, Fsize, Font);
                showPictureBox.Image = bitmap;

            }
            else if(ShapeBox.SelectedIndex == 0)//绘制圆形公章
            {
                
                int width = Convert.ToInt32(BreathUpDown.Text.ToString());
                int high = Convert.ToInt32(HeightUpDown.Text.ToString());
                int Lwidth = Convert.ToInt32(LineBreadthUpDown.Text.ToString());
                String center = CenterBox.Text.ToString();
                int centersize = Convert.ToInt32(CenterSize.Text.ToString());
                seal.CreateFrame_2(width, high, Lwidth, center, centersize);


                //我的水印
                //g.DrawString("支付宝账号：\n13568301905", myFont, new SolidBrush(Color.Black), bitmap.Width / 2 - 50, bitmap.Height / 2);

                
                int Hor = Convert.ToInt32(Horizontal.Text.ToString());
                int Ver = Convert.ToInt32(Vertical.Text.ToString());
                String str1 = inputText1.Text.ToString();
                String str2 = inputText2.Text.ToString();
                String str3 = inputText3.Text.ToString();

                char[] charArray = str3.ToCharArray();
                Array.Reverse(charArray);
                String str4 = new string(charArray);

                int CZ = Convert.ToInt32(CenterSizeBox.Text.ToString());
                String font = FontBox.Text.ToString();
                int FS=Convert.ToInt32(FontsizeBox.Text.ToString());
                double fsa= (double)Convert.ToInt32(fSweepAngle.Text.ToString());
                
                float w_h= Convert.ToSingle(WH_ratio.Text.ToString());
                float w_h2 = Convert.ToSingle(WH_ratio2.Text.ToString());

                bool bold = checkBox2.Checked ? true : false;
                bool bold2 = checkBox1.Checked ? true : false;
                seal.CreateFrame_2_C(Hor, Ver, str1, font, CZ, fsa,w_h,bold);

                seal.CreateText_2_D(width, high, str2, FS, font,bold2,w_h2);

                seal.CreateFrame_2_N(Hor+55, Ver+55, str4,font,100,bold);

                showPictureBox.Image = bitmap;

            }
            
        }



        /****************************************/
        /*************实时变化效果****************/
        /****************************************/

        private void CreateButton_Click(object sender, EventArgs e)//生成按钮响应程序
        {
            Draws();
        }

        private void LineBreadthUpDown_ValueChanged(object sender, EventArgs e)
        {
            //LineBreadthUpDown.Text = LineBreadthUpDown.Value.ToString();
            Draws();
        }

        private void BreathUpDown_ValueChanged(object sender, EventArgs e)
        {
            //BreathUpDown.Text = BreathUpDown.Value.ToString();
            Draws();
        }

        private void HeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            //HeightUpDown.Text = HeightUpDown.Value.ToString();
            Draws();
        }

        private void FontsizeBox_ValueChanged(object sender, EventArgs e)
        {
            //FontsizeBox.Text = FontsizeBox.Value.ToString();
            Draws();
        }

        private void CenterSizeBox_ValueChanged(object sender, EventArgs e)
        {
            //CenterSizeBox.Text = CenterSizeBox.Value.ToString();
            Draws();
        }

        private void inputText2_TextChanged(object sender, EventArgs e)     //即时改变文本框内容
        {
            Draws();
        }

        private void FontBox_SelectedIndexChanged(object sender, EventArgs e)       //即时改变字体
        {
            
            Draws();
        }

        private void ShapeBox_SelectedIndexChanged(object sender, EventArgs e)      //及时改变印章形状 
        {
            Draws();
        }
        private void CenterBox_SelectedIndexChanged(object sender, EventArgs e)//即时改变中心图案形状
        {
            Draws();
        }

        private void inputText1_TextChanged(object sender, EventArgs e) //即时改变文本框内容
        {
            Draws();
        }

        private void ScreenClr_Click(object sender, EventArgs e)      //清屏函数
        {
            showPictureBox.CreateGraphics().Clear(Color.White);
            //mousePath.Reset();
            //Draws();
        }
        private void Horizontal_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }

        private void Vertical_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void CenterSize_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void LineWidth2_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void fSweepAngle_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void WordWidth2_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void WordHeight2_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void WordHeight1_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void WordWidth1_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void WH_ratio_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void WH_ratio2_ValueChanged(object sender, EventArgs e)
        {
            Draws();
        }
        private void inputText3_TextChanged(object sender, EventArgs e)
        {
            Draws();
        }




        //保存按钮
        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveImageDialog.Filter = "图片(*.png)|*.png|图片(*.jpg)|*.jpg|所有文件|*.*";//设置文件类型
            saveImageDialog.FileName = "水印";
            saveImageDialog.DefaultExt = "png";//默认格式
            saveImageDialog.AddExtension = true;//设置自动在文件名中添加扩展名

            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveImageDialog.FilterIndex)
                {
                    case 1:
                        seal.changePixel();
                        //bitmap.MakeTransparent(Color.White);
                        bitmap.Save(saveImageDialog.FileName, ImageFormat.Png);
                        break;
                    case 2:                        
                        bitmap.Save(saveImageDialog.FileName, ImageFormat.Jpeg); break;
                }
                MessageBox.Show("保存成功!");
            }           

        }



        private void callObjectEvent(Object obj, string EventName, EventArgs e = null)
        {
            //建立一个类型      
            //Type t = typeof(obj.GetType);  
            Type t = Type.GetType(obj.GetType().AssemblyQualifiedName);
            //产生方法      
            MethodInfo m = t.GetMethod(EventName, BindingFlags.NonPublic | BindingFlags.Instance);
            //参数赋值。传入函数      
            //获得参数资料  
            ParameterInfo[] para = m.GetParameters();
            //根据参数的名字，拿参数的空值。  
            //参数对象      
            object[] p = new object[1];
            if (e == null)
                p[0] = Type.GetType(para[0].ParameterType.BaseType.FullName).GetProperty("Empty");
            else
                p[0] = e;
            //调用  
            m.Invoke(obj, p);
            return;
        }

        
    }
}
