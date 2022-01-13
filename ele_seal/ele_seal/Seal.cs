using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ele_seal
{
    class Seal
    {
        private Bitmap bitmap;
        private static int bitmapWidth, bitmapHeight;
        private static int frameWidth, frameHeight;

        private static int _letterspace = 0;//字体间距
        private static Char_Direction _chardirect = Char_Direction.Center;
        private static int _degree = 90;

        private static int circularity_W = 1;//设置圆画笔的粗细

       // private System.Drawing.Drawing2D.GraphicsPath mousePath = new System.Drawing.Drawing2D.GraphicsPath();

        //private static int space = 10;//比外面圆圈小

       


        public Seal(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            bitmapWidth = bitmap.Width;
            bitmapHeight = bitmap.Height;
        }
           


        //绘制矩形印章的框架
        public void CreateFrame_1(int width, int height, int frameSize)
        {
            frameWidth = width;
            frameHeight = height;
            Pen pen = new Pen(Color.Red, frameSize);    //初始化画笔的属性

            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            GraphicsPath gp = new GraphicsPath();
            Rectangle r = new Rectangle();
            r.Width = width;
            r.Height = height;
            r.X = bitmapWidth / 2 - width / 2;
            r.Y = bitmapHeight / 2 - height / 2;
            g.SmoothingMode = SmoothingMode.HighQuality;//平滑绘制，防锯齿
            //Rectangle rect1 = new Rectangle(circularity_W, circularity_W, bitmapWidth - circularity_W * 2, bitmapHeight - circularity_W * 2);//设置圆的绘制区域
            gp.AddRectangle(r);
            
            g.DrawPath(pen, gp);
            
        }

        //绘制矩形印章的文字
        public void CreateText_1(string text, int fontSize,String fontstyle,int LetterSpace,bool bold)
        {
            Graphics g = Graphics.FromImage(bitmap);
            Font font;
            if (bold)
            {
                font = new Font(fontstyle, fontSize,FontStyle.Bold);
            }
            else
            {
                font = new Font(fontstyle, fontSize);
            }

            SizeF sif = TextRenderer.MeasureText(text, font);
            float textWidth = sif.Width;
            float textHeight = sif.Height;

            Brush brush = new SolidBrush(Color.Red);
           // Pen pen = new Pen(Color.Red,framesize);    //初始化画笔的属性

            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            int actualPositionY =(int)(bitmapHeight - Convert.ToInt32(textHeight)) / 2;

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            //format.LineAlignment = StringAlignment.Center;

            g.SmoothingMode = SmoothingMode.HighQuality;//平滑绘制，防锯齿

           
            
            g.DrawString(text, font, brush, bitmapWidth / 2, actualPositionY, format);

        }


        //绘制圆形印章的框架
        public void CreateFrame_2(int width, int height, int frameSize, String star_Str,int Star_size)
        {
            frameWidth = width;
            frameHeight = height;
            Pen pen = new Pen(Color.Red, frameSize);    //初始化画笔的属性
   
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.HighQuality;//平滑绘制，防锯齿

            GraphicsPath gp = new GraphicsPath();
            Rectangle r = new Rectangle();
            r.Width = width;
            r.Height = height;
            r.X = bitmapWidth / 2 - width / 2;
            r.Y = bitmapHeight / 2 - height / 2;

            gp.AddRectangle(r);
            //g.DrawPath(pen, gp);

            //绘制星号
            g.SmoothingMode = SmoothingMode.HighQuality;//平滑绘制，防锯齿
            g.DrawEllipse(pen, r);
            //string star_Str = "★";
            PointF star_xy = new PointF(r.X, r.Y);

            Font star_Font = new Font("Arial", Star_size, FontStyle.Regular);//设置星号的字体样式

            //Font Var_Font = new Font("宋体", Star_size, FontStyle.Bold);
            SizeF star_Size = g.MeasureString(star_Str, star_Font);//对指定字符串进行测量
            //确认星号的位置
            float Cen_X = bitmapWidth / 2 - star_Size.Width / 2;
            float Cen_Y = bitmapHeight / 2 - star_Size.Height / 2;
            g.DrawString(star_Str, star_Font, pen.Brush, Cen_X, Cen_Y);

        }


        //绘制圆形印章的文字，其中company为环排文字，department为横排文字，number是印章编号（环排
        public void CreateText_2_D(int width, int height, string department,int fontSize2,String fontstyle,bool bold,float w_h)
        {
            Graphics g = Graphics.FromImage(bitmap);
            Font Var_Font;
            if (bold)
            {
                Var_Font = new Font(fontstyle, fontSize2, FontStyle.Bold);//定义部门字体的字体样式
            }
            else
            {
                Var_Font = new Font(fontstyle, fontSize2);//定义部门字体的字体样式
            }
            Brush brush = new SolidBrush(Color.Red);
            
            string var_txt = department;
            int var_len = var_txt.Length;          
            SizeF Var_Size = g.MeasureString(var_txt, Var_Font);//对指定字符串进行测量
              //在指定位置绘制横排文字
            g.SmoothingMode = SmoothingMode.HighQuality;//平滑绘制，防锯齿
            PointF Var_xy = new PointF(bitmapWidth / 2 - Var_Size.Width / 2, bitmapHeight / 2 - Var_Size.Height/4 + height/4);
            // - Var_Size.Width / 2
            
            // Init format
            /*StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;        

            GraphicsPath path = new GraphicsPath();
            //int x =(int) (bitmapWidth / 2 - textWidth / 2);          
            int x = (int)(bitmapWidth / 2);
            Font text_Font = new Font(fontstyle,fontSize2);//定义部门字体的字体样式
            path.AddString(department, text_Font.FontFamily,(int)text_Font.Style,text_Font.Size,Var_xy,sf);
            */

            Matrix m = new Matrix();
           
            m.Scale(w_h,1.0f);   //压扁         
            m.Translate((bitmapWidth/2)* (float)(1.0/w_h-1),0);
            g.Transform = m;
            //g.FillPath(brush, path);     
            g.DrawString(var_txt, Var_Font, brush, Var_xy);

        }


        public void CreateFrame_2_N(int width, int height, string number, string fontstyle,  double fSweepAngle, bool bold)
        {
            Graphics g = Graphics.FromImage(bitmap);
            Font num_Font;
            if (bold)
            {
                num_Font = new Font(fontstyle, 15, FontStyle.Bold);//定义部门字体的加粗字体样式
            }
            else
            {
                num_Font = new Font(fontstyle, 15);//定义部门字体的字体样式
            }
            Brush brush = new SolidBrush(Color.Red);

            Pen myPenbush = new Pen(Color.Red, circularity_W);


            Rectangle NewRect = new Rectangle();


            NewRect.Width = width;
            NewRect.Height = height;
            NewRect.X = bitmapWidth / 2 - width / 2;
            NewRect.Y = bitmapHeight / 2 - height / 2;

            //Pen pen = new Pen(Color.Red, 5);
            //g.DrawEllipse(pen, NewRect);

            string text_txt = number;
            int text_len = text_txt.Length;             //字符个数

            float[] fCharWidth = new float[text_len];
            //float fTotalWidth = ComputeStringLength(text_txt, g, fCharWidth, _letterspace, _chardirect, text_Font);

            double fStartAngle;

            //fSweepAngle = fTotalWidth * 360 / (NewRect.Width * Math.PI);   //张角
            float fTotalWidth = (float)(fSweepAngle * (NewRect.Width * Math.PI) / 360);

            for (int i = 0; i < text_len; i++)
            {
                fCharWidth[i] = fTotalWidth / (text_len);
            }

            fStartAngle = 270 - fSweepAngle / 2;                           //起始角度
            //fStartAngle = 180;
            PointF[] pntChars = new PointF[text_len];       //每个字符的位置坐标
            double[] fCharAngle = new double[text_len];     //每个字符的偏转角度
            ComputeCharPos_2(fCharWidth, pntChars, fCharAngle, fStartAngle, NewRect);

            g.SmoothingMode = SmoothingMode.HighQuality;//平滑绘制，防锯齿
            float w_h = 1;

            for (int i = 0; i < text_len; i++)
            {
                DrawRotatedText(g, text_txt[i].ToString(), (float)(fCharAngle[i] - 90), pntChars[i],num_Font, myPenbush, w_h);
            }

        }

        //public void CreateFrame_2_C(int width, int height,string company, Font text_Font, double fSweepAngle)
        public void CreateFrame_2_C(int width, int height, string company,string fontstyle,int fontSize1, double fSweepAngle,float w_h,bool bold)
        {
            Graphics g = Graphics.FromImage(bitmap);
            Font text_Font;
            if (bold)
            {
                text_Font = new Font(fontstyle, fontSize1, FontStyle.Bold);//定义部门字体的加粗字体样式
            }
            else
            {
                text_Font = new Font(fontstyle, fontSize1);//定义部门字体的字体样式
            }
            Brush brush = new SolidBrush(Color.Red);
            
            Pen myPenbush = new Pen(Color.Red, circularity_W);


            Rectangle NewRect = new Rectangle();
            

            NewRect.Width = width;
            NewRect.Height = height;
            NewRect.X = bitmapWidth / 2 - width / 2;
            NewRect.Y = bitmapHeight / 2 - height / 2;

           //Pen pen = new Pen(Color.Red, 5);
           //g.DrawEllipse(pen, NewRect);

            string text_txt = company;
            int text_len = text_txt.Length;             //字符个数
            
            float[] fCharWidth = new float[text_len];
            //float fTotalWidth = ComputeStringLength(text_txt, g, fCharWidth, _letterspace, _chardirect, text_Font);

            double fStartAngle;

            //fSweepAngle = fTotalWidth * 360 / (NewRect.Width * Math.PI);   //张角
            float fTotalWidth = (float)(fSweepAngle * (NewRect.Width * Math.PI) / 360);

            for(int i = 0; i < text_len; i++)
            {
                fCharWidth[i] = fTotalWidth / (text_len);
            }

            fStartAngle = 270-fSweepAngle / 2;                           //起始角度
            //fStartAngle = 0;
            PointF[] pntChars = new PointF[text_len];       //每个字符的位置坐标
            double[] fCharAngle = new double[text_len];     //每个字符的偏转角度
            ComputeCharPos(fCharWidth, pntChars, fCharAngle, fStartAngle,NewRect);

            g.SmoothingMode = SmoothingMode.HighQuality;//平滑绘制，防锯齿

            for (int i = 0; i < text_len; i++)
            {
                DrawRotatedText(g, text_txt[i].ToString(), (float)(fCharAngle[i]+90), pntChars[i], text_Font, myPenbush,w_h);
            }

        }

        //计算字符串的长度
       /* private static float ComputeStringLength(string sText, Graphics g, float[] fCharWidth, float fIntervalWidth, Char_Direction Direction, Font text_Font)
        {
            // Init字符串格式
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.None;
            sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap
            | StringFormatFlags.LineLimit;
            // 衡量整个字符串长度
            SizeF size = g.MeasureString(sText, text_Font, (int)text_Font.Style);
            RectangleF rect = new RectangleF(0f, 0f, size.Width, size.Height);
            // 测量每个字符大小
            CharacterRange[] crs = new CharacterRange[sText.Length];
            for (int i = 0; i < sText.Length; i++)
                crs[i] = new CharacterRange(i, 1);
            // 复位字符串格式
            sf.FormatFlags = StringFormatFlags.NoClip;
            sf.SetMeasurableCharacterRanges(crs);
            sf.Alignment = StringAlignment.Near;
            // 得到每一个字符大小
            Region[] regs = g.MeasureCharacterRanges(sText, text_Font, rect, sf);
            // Re-compute whole string length with space interval width
            float fTotalWidth = 0f;
            for (int i = 0; i < regs.Length; i++)
            {
                if (Direction == Char_Direction.Center || Direction == Char_Direction.OutSide)
                    fCharWidth[i] = regs[i].GetBounds(g).Width;
                else
                    fCharWidth[i] = regs[i].GetBounds(g).Height;
                fTotalWidth += fCharWidth[i] + fIntervalWidth;
            }
            fTotalWidth -= fIntervalWidth;//Remove the last interval width
            return fTotalWidth;
        }
        */

        //计算每个字符的位置
        private static void ComputeCharPos(float[] CharWidth, PointF[] recChars, double[] CharAngle, double StartAngle,Rectangle NewRect)
        {
            double fSweepAngle, fCircleLength;
            //Compute the circumference
            fCircleLength = NewRect.Width * Math.PI;//圆弧线的周长

            for (int i = 0; i < CharWidth.Length; i++)
            {
                //Get char sweep angle
                fSweepAngle = CharWidth[i] * 360 / fCircleLength;

                //Set point angle
                CharAngle[i] = StartAngle +fSweepAngle/2;

                //Get char position
                if (CharAngle[i] < 270f)
                    recChars[i] = new PointF(
                    NewRect.X + NewRect.Width / 2
                    - (float)(NewRect.Width / 2 *
                    Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                    NewRect.Y + NewRect.Width / 2
                    - (float)(NewRect.Width / 2 * Math.Cos(
                    Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));
                else
                    recChars[i] = new PointF(
                    NewRect.X + NewRect.Width / 2
                    + (float)(NewRect.Width / 2 *
                    Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                    NewRect.Y + NewRect.Width / 2
                    - (float)(NewRect.Width / 2 * Math.Cos(
                    Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));

                //Get total sweep angle with interval space
                fSweepAngle = (CharWidth[i] ) * 360 / fCircleLength;
                StartAngle += fSweepAngle;

            }
        }
        private static void ComputeCharPos_2(float[] CharWidth, PointF[] recChars, double[] CharAngle, double StartAngle, Rectangle NewRect)
        {
            double fSweepAngle, fCircleLength;
            //Compute the circumference
            fCircleLength = NewRect.Width * Math.PI;//圆弧线的周长
            StartAngle += 180;
            for (int i = 0; i < CharWidth.Length; i++)
            {
                //Get char sweep angle
                fSweepAngle = CharWidth[i] * 360 / fCircleLength;

                //Set point angle
                CharAngle[i] = StartAngle + fSweepAngle / 2;

                //Get char position
                if (CharAngle[i] < 270f)
                    recChars[i] = new PointF(
                    NewRect.X + NewRect.Width / 2
                    - (float)(NewRect.Width / 2 *
                    Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                    NewRect.Y + NewRect.Width / 2
                    - (float)(NewRect.Width / 2 * Math.Cos(
                    Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));
                else
                    recChars[i] = new PointF(
                    NewRect.X + NewRect.Width / 2
                    + (float)(NewRect.Width / 2 *
                    Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                    NewRect.Y + NewRect.Width / 2
                    - (float)(NewRect.Width / 2 * Math.Cos(
                    Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));

                //Get total sweep angle with interval space
                fSweepAngle = (CharWidth[i]) * 360 / fCircleLength;
                StartAngle += fSweepAngle;
                

            }
        }

        //按照规定位置绘制每个字符
        private static void DrawRotatedText(Graphics g, string _text, float _angle, PointF text_Point, Font text_Font, Pen myPen,float WH)
        {
            // Init format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            // Create graphics path
            GraphicsPath gp = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
            int x = (int)text_Point.X;
            int y = (int)text_Point.Y;

            // Add string
            gp.AddString(_text, text_Font.FontFamily, (int)text_Font.Style, text_Font.Size, new Point(x, y), sf);

            SizeF charSize = g.MeasureString(_text, text_Font);

            Rectangle NewRect = new Rectangle();
            NewRect.Width = (int)charSize.Width;
            NewRect.Height =(int)charSize.Height;

            // Rotate string and draw it
            Matrix m = new Matrix();
              

            m.RotateAt(_angle, new PointF(x, y));
           
            switch (WH)
            {
                case 0.5f: 
                    m.Scale(WH, 1.0f);  
                    m.Translate(x, 0);
                    break;
                case 0.6f:
                    m.Scale(WH, 1.0f); 
                    m.Translate(x / (float)1.5, 0);
                    break;
                case 0.7f: 
                    m.Scale(WH, 1.0f); 
                    m.Translate(x / (float)2.4, 0); 
                    break;
                case 0.8f: 
                    m.Scale(WH, 1.0f); 
                    m.Translate(x / (float)4.1, 0);
                    break;
                case 0.9f:
                    m.Scale(WH, 1.0f);
                    m.Translate(x / (float)10, 0); 
                    break;
                case 1.0f:
                    m.Scale(WH, 1.0f);
                    m.Translate(0, 0); 
                    break;
                default:break;
            }
                
                
 


            g.Transform = m;
            
            //g.ResetTransform();

            //g.DrawPath(myPen, gp);
            g.FillPath(new SolidBrush(Color.Red), gp);
        }

        

        public enum Char_Direction
        {
            Center = 0,
            OutSide = 1,
            ClockWise = 2,
            AntiClockWise = 3,
        }
               

        public void changePixel()
        {
            for (int i = 0; i < bitmapWidth; i++)
            {
                for (int j = 0; j < bitmapHeight; j++)
                {
                    Color c = bitmap.GetPixel(i, j);
                    float light = c.GetBrightness();
                    //MessageBox.Show(""+light);
                    bitmap.SetPixel(i, j, Color.FromArgb((int)((1 - light) * 255), c));
                }
            }
        }

    }   
}
