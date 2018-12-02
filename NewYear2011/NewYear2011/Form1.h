#pragma once

#include <stdio.h>
#include <ctype.h>

namespace NewYear2011 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for Form1
	///
	/// WARNING: If you change the name of this class, you will need to change the
	///          'Resource File Name' property for the managed resource compiler tool
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Panel^  DedMoroz;
	private: System::Windows::Forms::TextBox^  TBOtvet;
	private: System::Windows::Forms::Label^  LabelLocation;

	private: System::Windows::Forms::Button^  ButtonOtvet;
	private: System::Windows::Forms::Label^  LabelResult;


	protected: 
	protected: 
	protected: 
	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(Form1::typeid));
			this->DedMoroz = (gcnew System::Windows::Forms::Panel());
			this->TBOtvet = (gcnew System::Windows::Forms::TextBox());
			this->LabelLocation = (gcnew System::Windows::Forms::Label());
			this->ButtonOtvet = (gcnew System::Windows::Forms::Button());
			this->LabelResult = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// DedMoroz
			// 
			this->DedMoroz->BackColor = System::Drawing::Color::Transparent;
			this->DedMoroz->BackgroundImage = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"DedMoroz.BackgroundImage")));
			this->DedMoroz->BackgroundImageLayout = System::Windows::Forms::ImageLayout::Stretch;
			this->DedMoroz->Location = System::Drawing::Point(506, 0);
			this->DedMoroz->Name = L"DedMoroz";
			this->DedMoroz->Size = System::Drawing::Size(218, 376);
			this->DedMoroz->TabIndex = 0;
			// 
			// TBOtvet
			// 
			this->TBOtvet->BackColor = System::Drawing::Color::White;
			this->TBOtvet->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->TBOtvet->ForeColor = System::Drawing::Color::Black;
			this->TBOtvet->Location = System::Drawing::Point(93, 338);
			this->TBOtvet->Multiline = true;
			this->TBOtvet->Name = L"TBOtvet";
			this->TBOtvet->Size = System::Drawing::Size(407, 26);
			this->TBOtvet->TabIndex = 1;
			this->TBOtvet->Text = L"1234567890";
			// 
			// LabelLocation
			// 
			this->LabelLocation->BackColor = System::Drawing::Color::Transparent;
			this->LabelLocation->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 18, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->LabelLocation->ForeColor = System::Drawing::Color::White;
			this->LabelLocation->Location = System::Drawing::Point(9, 9);
			this->LabelLocation->Name = L"LabelLocation";
			this->LabelLocation->Size = System::Drawing::Size(491, 292);
			this->LabelLocation->TabIndex = 2;
			this->LabelLocation->Text = L"Location";
			// 
			// ButtonOtvet
			// 
			this->ButtonOtvet->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->ButtonOtvet->Location = System::Drawing::Point(12, 338);
			this->ButtonOtvet->Name = L"ButtonOtvet";
			this->ButtonOtvet->Size = System::Drawing::Size(75, 27);
			this->ButtonOtvet->TabIndex = 3;
			this->ButtonOtvet->Text = L"Ответ";
			this->ButtonOtvet->UseVisualStyleBackColor = true;
			this->ButtonOtvet->Click += gcnew System::EventHandler(this, &Form1::ButtonOtvet_Click);
			// 
			// LabelResult
			// 
			this->LabelResult->BackColor = System::Drawing::Color::Transparent;
			this->LabelResult->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 18, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->LabelResult->ForeColor = System::Drawing::Color::White;
			this->LabelResult->Location = System::Drawing::Point(9, 301);
			this->LabelResult->Name = L"LabelResult";
			this->LabelResult->Size = System::Drawing::Size(491, 34);
			this->LabelResult->TabIndex = 4;
			this->LabelResult->Text = L"Result";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackgroundImage = (cli::safe_cast<System::Drawing::Image^  >(resources->GetObject(L"$this.BackgroundImage")));
			this->BackgroundImageLayout = System::Windows::Forms::ImageLayout::Stretch;
			this->ClientSize = System::Drawing::Size(724, 376);
			this->Controls->Add(this->LabelResult);
			this->Controls->Add(this->ButtonOtvet);
			this->Controls->Add(this->LabelLocation);
			this->Controls->Add(this->TBOtvet);
			this->Controls->Add(this->DedMoroz);
			this->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->Icon = (cli::safe_cast<System::Drawing::Icon^  >(resources->GetObject(L"$this.Icon")));
			this->MaximizeBox = false;
			this->Name = L"Form1";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"С новым годом!";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			this->ResumeLayout(false);
			this->PerformLayout();

		}

#pragma endregion


int Number;
int Iteration;

private: System::Void Form1_Load(System::Object^  sender, System::EventArgs^  e) {
	System::Media::SoundPlayer ^ f;
	f=gcnew System::Media::SoundPlayer("E:\\Егор\\Школа\\Университет\\Лабораторная C++\\NewYear2011\\Media\\Start_Song.wav");
	f->Play(); 

	Number = 12;
	Iteration = 0;

	this->TBOtvet->Text = "";
	this->LabelLocation->Text = "            ПРИВЕТ, ПОЛИНА!\n";
	this->LabelLocation->Text += "Ты готова отгадать все загадки и справиться с заданиями? В конце тебя ждёт хороший подарок! Я думаю, что ты сможешь справиться с ними сама, без подсказок.\n\n";
	this->LabelLocation->Text += "Чтобы начать, нажми кнопку";
	this->LabelResult->Text = "''Ответ''";
}



private: System::Void ButtonOtvet_Click(System::Object^  sender, System::EventArgs^  e) {
	String^ Otvet;
	if (Iteration == 1){
		Otvet = "аквариум";
	}else if (Iteration == 2){
		Otvet = "булка";
	}else if (Iteration == 3){
		Otvet = "дятел";
	}else if (Iteration == 4){
		Otvet = "ласточка";
	}else if (Iteration == 5){
		Otvet = "мяч";
	}else if (Iteration == 6){
		Otvet = "гайка";
	}else if (Iteration == 7){
		Otvet = "28";
	}else if (Iteration == 8){
		Otvet = "удочка";
	}else if (Iteration == 9){
		Otvet = "игрушки";
	}else if (Iteration == 10){
		Otvet = "лыжи";
	}else if (Iteration == 11){
		Otvet = "груша";
	}else if (Iteration == 12){
		Otvet = "4";
	}

	if (Iteration == 0){
		Iteration++;
		this->LabelLocation->Text = "Ищи записку на подоконнике в своей комнате";
		this->LabelResult->Text = "";
	}else if (Otvet == this->TBOtvet->Text){
			this->LabelResult->Text = "ВЕРНО!!!";
			Iteration++;
			System::Media::SoundPlayer^ a;
			a=gcnew System::Media::SoundPlayer("E:\\Егор\\Школа\\Университет\\Лабораторная C++\\NewYear2011\\Media\\Applause.wav");
			a->Play(); 
		}else{
			this->LabelResult->Text = "Неверно, попробуй ещё раз";
		}
	

	if (Iteration == 2){
		this->LabelLocation->Text = "Я готовить не люблю,\n";
		this->LabelLocation->Text += "А поесть - пожалуйста!\n";
		this->LabelLocation->Text += "Можно в гости к Вам приду?\n"; 
		this->LabelLocation->Text += "А, гостей Вы балуете?\n\n";
		this->LabelLocation->Text += "Может в стол заглянешь?";
	}else if (Iteration == 3){
		this->LabelLocation->Text = "Я знаю, что ты смотришь его днём и ночью.";
		this->LabelLocation->Text += "\n\nИщи на нём";
	}else if (Iteration == 4){
		this->LabelLocation->Text = "Хочу гулять, гулять, гулять!\n";
		this->LabelLocation->Text += "Мне надо шапку одевать!\n";
		this->LabelLocation->Text += "Мне время шапку одевать,\n";
		this->LabelLocation->Text += "Когда давно пора гулять!";
		this->LabelLocation->Text += "\n\nНайди записку";
	}else if (Iteration == 5){
		this->LabelLocation->Text = "Твои игрушки уже давно заснули в комоде!";
	}else if (Iteration == 6){
		this->LabelLocation->Text = "Ты уже первоклассница!\n";
		this->LabelLocation->Text += "А уроки где учишь?";
	}else if (Iteration == 7){
		this->LabelLocation->Text = "Рано утром на рассвете\n";
		this->LabelLocation->Text += "Умываются мышата,\n";
		this->LabelLocation->Text += "И котята, и утята,\n";
		this->LabelLocation->Text += "И жучки, и паучки.";
		this->LabelLocation->Text += "\n\nГде записка?";
	}else if (Iteration == 8){
		this->LabelLocation->Text = "Полюбуйся, посмотри -\n";
		this->LabelLocation->Text += "Полюс северный внутри!\n";
		this->LabelLocation->Text += "Там сверкает снег и лед,\n";
		this->LabelLocation->Text += "Там сама зима живет.\n";
		this->LabelLocation->Text += "Навсегда нам эту зиму\n";
		this->LabelLocation->Text += "Привезли из магазина.";
		this->LabelLocation->Text += "\n\nКак думаешь, где записка";
	}else if (Iteration == 9){
		this->LabelLocation->Text = "Где у тебе лежат носочки?\n";
		this->LabelLocation->Text += "Там ты найдёшь платочек!";
	}else if (Iteration == 10){
		this->LabelLocation->Text = "Где ты разогреваешь суп?";
	}else if (Iteration == 11){
		this->LabelLocation->Text = "Ринг - наша жизнь - и мы есть в нем.\n";
		this->LabelLocation->Text += "И каждый раунд, как мгновенье,-\n";
		this->LabelLocation->Text += "Не устоишь перед ''врагом'',\n";
		this->LabelLocation->Text += "Не миновать тебе паденья.";
		this->LabelLocation->Text += "\n\nНайди записку";
	}else if (Iteration == 12){
		this->LabelLocation->Text = "А в чём твой брат носит учебники и тетрадки";
	}else if (Iteration == 13){
		this->LabelLocation->Text = "Ты разве не знаешь, где Дед Мороз прячет подарки?";
	}


}




};
}

