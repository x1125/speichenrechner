using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Speichenrechner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Speichenrechner v1.0\n\nEntwickelt von Xore Solutions\nGrafiken, Texte und Logik von Sapim.be", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // RADIANS 2 DEGREES
		private float ra_de(float val) {
			double pi = Math.PI;
			var de = (val*(180/pi));
			return (float)de;
		}
		// DEGREES 2 RADIANS
		private float de_ra(float val) {
			double pi = Math.PI;
			var ra = (val*(pi/180));
			return (float)ra;
		}

        private void calcButton_Click(object sender, EventArgs e)
        {
            int totalLengthHub, nonGearSide, gearSide, diameterNonGearSide, diameterGearSide, internalDiameterRim, rimThickness, numberSpokes, crossingsGear, crossingsNonGear;
            float aTandwiel, aParaplu, result1, result2;
            double aTandwiel_tussenstap, aParaplu_tussenstap;

            try
            {
                totalLengthHub = int.Parse(this.totalLengthHub.Text, System.Globalization.CultureInfo.InvariantCulture);
                nonGearSide = int.Parse(this.nonGearSide.Text, System.Globalization.CultureInfo.InvariantCulture);
                gearSide = int.Parse(this.gearSide.Text, System.Globalization.CultureInfo.InvariantCulture);
                diameterNonGearSide = int.Parse(this.hubDiameterNonGear.Text, System.Globalization.CultureInfo.InvariantCulture);
                diameterGearSide = int.Parse(this.hubDiameterGear.Text, System.Globalization.CultureInfo.InvariantCulture);
                internalDiameterRim = int.Parse(this.internalDiameter.Text, System.Globalization.CultureInfo.InvariantCulture);
                rimThickness = int.Parse(this.rimThickness.Text, System.Globalization.CultureInfo.InvariantCulture);
                numberSpokes = int.Parse(this.numberOfSpokes.Text, System.Globalization.CultureInfo.InvariantCulture);
                crossingsGear = int.Parse(this.numberOfCrossings.Text, System.Globalization.CultureInfo.InvariantCulture);
                crossingsNonGear = int.Parse(this.numberOfCrossingsNonGear.Text, System.Globalization.CultureInfo.InvariantCulture);

                if (
                    totalLengthHub < 1 || nonGearSide < 1 || gearSide < 1 ||
                    diameterNonGearSide < 1 || diameterGearSide < 1 || internalDiameterRim < 1 ||
                    rimThickness < 1 || numberSpokes < 1 || crossingsGear < 1 || crossingsNonGear < 1
                )
                    throw new Exception();

                aTandwiel = (totalLengthHub / 2) - nonGearSide;
                aTandwiel_tussenstap =      (Math.Pow(internalDiameterRim / 2, 2) +
                                            Math.Pow(diameterNonGearSide / 2, 2) +
                                            Math.Pow(aTandwiel, 2) - (internalDiameterRim * (diameterNonGearSide / 2) * (Math.Cos(de_ra((720 * crossingsNonGear) / numberSpokes)))));

                double aTandwiel_tussenstap_round = Math.Round(Math.Sqrt(aTandwiel_tussenstap));
                float spaaklengteTandwiel = (float)aTandwiel_tussenstap_round + (float)rimThickness;
                result1 = spaaklengteTandwiel;

                aParaplu = (totalLengthHub / 2) - gearSide;
                aParaplu_tussenstap =
                                    (Math.Pow(internalDiameterRim / 2, 2) +
                                    Math.Pow(diameterGearSide / 2, 2) +
                                    Math.Pow(aParaplu, 2)) -
                                    (internalDiameterRim * (diameterGearSide / 2) * Math.Cos(de_ra((720 * crossingsGear) / numberSpokes)));
                double aParaplu_tussenstap_round = Math.Round(Math.Sqrt(aParaplu_tussenstap));
                float spaaklengteParaplu = (float)aParaplu_tussenstap_round + (float)rimThickness;
                result2 = spaaklengteParaplu;

                MessageBox.Show(this, "Spoke length non-gear side: " + result1 + " mm\nSpoke length gear side: " + result2 + "mm", "Ergebnis", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Bitte füllen Sie alle Felder aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
