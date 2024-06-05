namespace CodeGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_validator = new Button();
            button1_validate_module = new Button();
            button_autofac = new Button();
            SuspendLayout();
            // 
            // button_validator
            // 
            button_validator.Location = new Point(55, 12);
            button_validator.Name = "button_validator";
            button_validator.Size = new Size(210, 29);
            button_validator.TabIndex = 0;
            button_validator.Text = "Validator Generate";
            button_validator.UseVisualStyleBackColor = true;
            button_validator.Click += button_validator_Click;
            // 
            // button1_validate_module
            // 
            button1_validate_module.Location = new Point(55, 47);
            button1_validate_module.Name = "button1_validate_module";
            button1_validate_module.Size = new Size(210, 29);
            button1_validate_module.TabIndex = 1;
            button1_validate_module.Text = "Validator Module Generater";
            button1_validate_module.UseVisualStyleBackColor = true;
            button1_validate_module.Click += button1_validate_module_Click;
            // 
            // button_autofac
            // 
            button_autofac.Location = new Point(55, 82);
            button_autofac.Name = "button_autofac";
            button_autofac.Size = new Size(210, 29);
            button_autofac.TabIndex = 2;
            button_autofac.Text = "autofac Generater";
            button_autofac.UseVisualStyleBackColor = true;
            button_autofac.Click += button_autofac_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(639, 368);
            Controls.Add(button_autofac);
            Controls.Add(button1_validate_module);
            Controls.Add(button_validator);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button_validator;
        private Button button1_validate_module;
        private Button button_autofac;
    }
}
