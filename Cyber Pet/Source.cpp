#include <iostream>
#include <chrono>
#include <thread>
#include <Windows.h> //used to get state of keyboard
#include <fstream> //used for input/output

using namespace std::this_thread; // sleep_for, sleep_until
using namespace std::chrono; // nanoseconds, system_clock, seconds
using namespace std;


class petAttributes
{
	private: 
		int hunger; //Used to increase pet's hunger
		int tiredness; //Used to increase pet's tiredness
		int happiness; //Used to increase pet's happiness
		int hydration; //Used to increase pet's hydration
		int boredom; //Used to increase pet's boredom

		int timeSinceFed = 0; //Used to count to a set amount (where hunger will decrease by 1)
		int timeSinceSlept = 0; //Used to count to a set amount (where tiredness will decrease by 1)
		int timeSinceWalked = 0; //Used to count to a set amount (where boredom will decrease by 1)
		int timeSinceHydrated = 0; //Used to count to a set amount (where hydration will decrease by 1)

		string happinessStatus; //Used to store state of pets happiness
		string hungerStatus; //Used to store state of pets hunger
		string tirednessStatus; //Used to store state of pets tiredness
		string hydrationStatus; //Used to store state of pets hydration
		string boredomStatus; //Used to store state of pets boredom

		string petName; //Used to store pets name

	public:

		//Pet attributes
		//Setters
		void setHunger(int inputHunger)
		{
			hunger = inputHunger;
		}
		void setTiredness(int inputTiredness)
		{
			tiredness = inputTiredness;
		}
		void setHappiness(int inputHappiness)
		{
			happiness = inputHappiness;
		}
		void setHydration(int inputHydration)
		{
			hydration = inputHydration;
		}
		void setBoredom(int inputBoredom)
		{
			boredom = inputBoredom;
		}
		void setName(string inputName)
		{
			cout << "What is your pet called?" << endl;
			cin >> petName;
		}


		//Getters
		int getHunger()
		{
			return hunger;
		}
		int getTiredness()
		{
			return tiredness;
		}
		int getHappiness()
		{
			return happiness;
		}
		int getHydration()
		{
			return hydration;
		}
		int getBoredom()
		{
			return boredom;
		}
		string getName()
		{
			return petName;
		}



		//Pet status
		//Setters
		void setHungerStatus(string inputHunger)
		{
			hungerStatus = inputHunger;
		}
		void setTirednessStatus(string inputTiredness)
		{
			tirednessStatus = inputTiredness;
		}
		void setHappinessStatus(string inputHappiness)
		{
			happinessStatus = inputHappiness;
		}
		void setHydrationStatus(string inputHydration)
		{
			hydrationStatus = inputHydration;
		}
		void setBoredomStatus(string inputBoredom)
		{
			boredomStatus = inputBoredom;
		}


		//Getters
		string getHungerStatus()
		{
			return hungerStatus;
		}
		string getTirednessStatus()
		{
			return tirednessStatus;
		}
		string getHappinessStatus()
		{
			return happinessStatus;
		}
		string getHydrationStatus()
		{
			return hydrationStatus;
		}
		string getBoredomStatus()
		{
			return boredomStatus;
		}



		//Time since
		//Setters
		void setLastFed(int inputHunger)
		{
			timeSinceFed = inputHunger;
		}
		void setLastSlept(int inputTiredness)
		{
			timeSinceSlept = inputTiredness;
		}
		void setLastWalked(int inputHappiness)
		{
			timeSinceWalked = inputHappiness;
		}
		void setLastHydrated(int inputHydration)
		{
			timeSinceHydrated = inputHydration;
		}


		//Getters
		int getLastFed()
		{
			return timeSinceFed;
		}
		int getLastSlept()
		{
			return timeSinceSlept;
		}
		int getLastWalked()
		{
			return timeSinceWalked;
		}
		int getLastHydrated()
		{
			return timeSinceHydrated;
		}


		//Initialises object's attributes
		void petConstructor(int hunger, int tiredness, int happiness, int hydration, int boredom)
		{
			//Pet attributes
			hunger;
			tiredness;
			happiness;
			hydration;
			boredom;
			petName = "your pet";

			//Time since
			timeSinceFed = 0;
			timeSinceSlept = 0;
			timeSinceWalked = 0;
			timeSinceHydrated = 0;
		}

};

//Functions used to....
void petMenu(petAttributes); //used for main part of program
void newPet(); //Creates new pet
void initialisePetAttributes(); //Assigns values to pet's attributes
void viewPets(); //Displays menu with all pet names 

int changeHappiness(petAttributes); //used to change petHappiness variable
void feedPet(petAttributes&); //used to increase petHunger variable by 1
void letPetSleep(petAttributes&); //used to increase petTiredness variable by 1
void walkPet(petAttributes&); //used to increase petBoredom variable by 1
void hydratePet(petAttributes&); //used to increase petHydration variable by 1
void petPet(petAttributes, int);  //displays pet's reaction to being petted (based on it's happiness)
void displayPetStatus(petAttributes); //used to display pet's hunger, tiredness, happiness, hydration 

int readFromFile(); //used to read data from file
void writeToFile(int, int, int, int); //used to write data to file

//Creates pet objects (slightly inefficient as not all will be used in certain instances)
petAttributes pet1;
petAttributes pet2;
petAttributes pet3;
petAttributes pet4;
petAttributes pet5;
petAttributes pet6;
petAttributes pet7;
petAttributes pet8;

int numberOfPets = 0; //Used to store how many pets user has 
string fileLocation; //Used to store location of text document that each pet has their attribute values stored in

//These arrays are used to allow the program to display how the pet is feeling
string hungerChoice[5] = { "Dead", "Starving", "Rather Hungry", "Slightly Peckish", "Well Fed" }; //Different variations of hunger
string tirednessChoice[5] = { "Collapsed", "Falling Asleep", "Tired", "Awake", "Wide Awake" }; //Different variations of tiredness
string happinessChoice[5] = { "Angry", "Upset", "Contempt", "Happy", "Ecstatic" }; //Different variations of happiness 
string hydrationChoice[5] = { "Dead", "Parched", "Thirsty", "Fairly hydrated", "Hydrated" }; //Different variations of hydration
string boredomChoice[5] = { "Lethargic", "Bored", "Interested", "Lively", "Excited" }; //Different variations of boredom


int main()
{
	//Variable is used to dictate when condition loop is ended
	bool leaveMenu = false;

	//Whilst user doesn't want to end program
	while (leaveMenu == false)
	{
		//'Reset' command terminal (this uses ANSI escape codes to clear the command console)
		//Html ref: https://www.delftstack.com/howto/cpp/how-to-clear-console-cpp/
		cout << "\x1B[2J\x1B[H";

		//Display menu
		cout << "1 - Create new pet";
		cout << " | 2 - View all pets";
		cout << " | 3 - End Program \n";


		//Delay for a second to prevent menu from displaying improperly
		sleep_for(seconds(1));


		if (GetKeyState('1') & 0x1000)
		{
			//Calls function allowing user to create a new pet
			newPet();
		}
		else if (GetKeyState('2') & 0x1000)
		{
			//If user has created a pet
			if (numberOfPets != 0)
			{
				//Assign values to each pet
				initialisePetAttributes();

				//Calls function displaying all pets
				viewPets();
			}
			//If user hasn't created a pet
			else
			{
				//Display error message
				cout << "You have not created any pets yet, create a pet and try again" << endl;

				//Delay for 3 seconds to let user read message
				sleep_for(seconds(3));
			}
		}
		else if (GetKeyState('3') & 0x1000)
		{
			//Ends program
			cout << "Quitting program \n" << endl;

			//Delay for 3 seconds to let user read message
			sleep_for(seconds(3));

			//Allows conditional loop to end (and user to exit program)
			leaveMenu = true;
		}

	}
	
	return 0;
	
}


void newPet()
{

	//If user hasn't created 8 pets (user can only create 9 pets)
	if (numberOfPets < 8)
	{
		//Increase number of pets
		numberOfPets++;

		//Create new pet
		//I attempted to use switch(case), but VS kept throwing error which said:
		//"C++ transfer of control bypasses initialization of: variable".
		//So I've used else if statements instead
		if (numberOfPets == 1)
		{
			//Ask user for pets name 
			pet1.setName(pet1.getName());
		}
		else if (numberOfPets == 2)
		{
			pet2.setName(pet2.getName());
		}
		else if (numberOfPets == 3)
		{
			pet3.setName(pet3.getName());
		}
		else if (numberOfPets == 4)
		{
			pet4.setName(pet4.getName());
		}
		else if (numberOfPets == 5)
		{
			pet5.setName(pet5.getName());
		}
		else if (numberOfPets == 6)
		{
			pet6.setName(pet6.getName());
		}
		else if (numberOfPets == 7)
		{
			pet7.setName(pet7.getName());
		}
		else if (numberOfPets == 8)
		{
			pet8.setName(pet8.getName());
		}
		

	}
	//If user has already created the maximum number of pets
	else
	{
		//Display message
		cout << "You already have the maximum number of pets!" << endl;

		//Delay for 3 seconds to let user read message
		sleep_for(seconds(3));
	}


}



void initialisePetAttributes()
{

	//Repeat for number of pets (and depending on the number of pets created, only some of these cases will be met)
	for (int numbers = 1; numbers < (numberOfPets + 1); numbers++)
	{

		//Change case based on iteration of loop
		switch (numbers)
		{
		case 1:
			//Set file location to that of pet 1 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet1.txt";
			//Read values from file to each pet1 attribute
			pet1.setHunger(readFromFile());
			pet1.setTiredness(readFromFile());
			pet1.setHydration(readFromFile());
			pet1.setBoredom(readFromFile());
			break;

		case 2:
			//Set file location to that of pet 2 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet2.txt";
			//Read values from file to each pet2 attribute
			pet2.setHunger(readFromFile());
			pet2.setTiredness(readFromFile());
			pet2.setHydration(readFromFile());
			pet2.setBoredom(readFromFile());
			break;

		case 3:
			//Set file location to that of pet 3 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet3.txt";
			//Read values from file to each pet3 attribute
			pet3.setHunger(readFromFile());
			pet3.setTiredness(readFromFile());
			pet3.setHydration(readFromFile());
			pet3.setBoredom(readFromFile());
			break;

		case 4:
			//Set file location to that of pet 4 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet4.txt";
			//Read values from file to each pet4 attribute
			pet4.setHunger(readFromFile());
			pet4.setTiredness(readFromFile());
			pet4.setHydration(readFromFile());
			pet4.setBoredom(readFromFile());
			break;

		case 5:
			//Set file location to that of pet 5 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet5.txt";
			//Read values from file to each pet5 attribute
			pet5.setHunger(readFromFile());
			pet5.setTiredness(readFromFile());
			pet5.setHydration(readFromFile());
			pet5.setBoredom(readFromFile());
			break;

		case 6:
			//Set file location to that of pet 6 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet6.txt";
			//Read values from file to each pet6 attribute
			pet6.setHunger(readFromFile());
			pet6.setTiredness(readFromFile());
			pet6.setHydration(readFromFile());
			pet6.setBoredom(readFromFile());
			break;

		case 7:
			//Set file location to that of pet 7 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet7.txt";
			//Read values from file to each pet7 attribute
			pet7.setHunger(readFromFile());
			pet7.setTiredness(readFromFile());
			pet7.setHydration(readFromFile());
			pet7.setBoredom(readFromFile());
			break;

		case 8:
			//Set file location to that of pet 8 text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet8.txt";
			//Read values from file to each pet8 attribute
			pet8.setHunger(readFromFile());
			pet8.setTiredness(readFromFile());
			pet8.setHydration(readFromFile());
			pet8.setBoredom(readFromFile());
			break;

		}

	}

}


//Displays menu showing all pets
void viewPets()
{
	bool leaveMenu = false;

	//Whilst user doesn't want to return to start menu
	while (leaveMenu == false)
	{

		cout << "\x1B[2J\x1B[H";

		//Display menu
		for (int numbers = 1; numbers < numberOfPets + 1; numbers++)
		{

			switch (numbers)
			{
			//Gets pets name from user
			case 1:
				cout << "1 " << pet1.getName();
				break;

			case 2:
				cout << " | 2 " << pet2.getName();
				break;

			case 3:
				cout << " | 3 " << pet3.getName();
				break;

			case 4:
				cout << " | 4 " << pet4.getName();
				break;

			case 5:
				cout << " | 5 " << pet5.getName();
				break;

			case 6:
				cout << " | 6 " << pet6.getName();
				break;

			case 7:
				cout << " | 7 " << pet7.getName();
				break;

			case 8:
				cout << " | 8 " << pet8.getName();
				break;

			}

		}


		cout << " | 9 return to previous menu" << endl;
	

		sleep_for(seconds(1));

		//If user presses key 1:
		if (GetKeyState('1') & 0x8000)
		{
			//Since we will be writing pet attributes to file, we will need the location of the correct text file
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet1.txt";
			//Pass pet 1 to function
			petMenu(pet1);
		}
		//If user presses key 2:
		else if (GetKeyState('2') & 0x8000)
		{
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet2.txt";
			//Pass pet 2 to function
			petMenu(pet2);
		}
		//If user presses key 3:
		else if (GetKeyState('3') & 0x8000)
		{
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet3.txt";
			//Pass pet 3 to function
			petMenu(pet3);
		}
		//If user presses key 4:
		else if (GetKeyState('4') & 0x8000)
		{
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet4.txt";
			//Pass pet 4 to function
			petMenu(pet4);
		}
		//If user presses key 5:
		else if (GetKeyState('5') & 0x8000)
		{
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet5.txt";
			//Pass pet 5 to function
			petMenu(pet5);
		}
		//If user presses key 6:
		else if (GetKeyState('6') & 0x8000)
		{
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet6.txt";
			//Pass pet 6 to function
			petMenu(pet6);
		}
		//If user presses key 7:
		else if (GetKeyState('7') & 0x8000)
		{
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet7.txt";
			//Pass pet 7 to function
			petMenu(pet7);
		}
		//If user presses key 8:
		else if (GetKeyState('8') & 0x8000)
		{
			fileLocation = "C:/Users/LeeA1/Documents/School Work/Uni/CMP104/Programs/Cyber Pet Text Files/pet8.txt";
			//Pass pet 8 to function
			petMenu(pet8);
		}
		//If user presses key 9:
		else if (GetKeyState('9') & 0x8000)
		{
			//Display message
			cout << "Returning to start menu" << endl;

			//Delay for 3 seconds to let user read message
			sleep_for(seconds(3));

			leaveMenu = true;
		}

	}

}


void petMenu(petAttributes pet)
{
	int petReaction = 0; //Used to dictate how pet reacts to user petting them
	bool leaveMenu = false;

	//While user doesn't want to return to pet menu
	while (leaveMenu == false)
	{
		cout << "\x1B[2J\x1B[H";


		//Use integer variables (eg: happiness) to assign corresponding string to string variables
		pet.setHappinessStatus(happinessChoice[pet.getHappiness()]);
		pet.setHungerStatus(hungerChoice[pet.getHunger()]);
		pet.setTirednessStatus(tirednessChoice[pet.getTiredness()]);
		pet.setHydrationStatus(hydrationChoice[pet.getHydration()]);
		pet.setBoredomStatus(boredomChoice[pet.getBoredom()]);


		//Update happiness
		pet.setHappiness(changeHappiness(pet));


		//Display menu
		cout << "1 - Feed " << pet.getName();
		cout << "| 2 - Give " << pet.getName() << " a nap";
		cout << "| 3 - Give " << pet.getName() << " a drink";
		cout << "| 4 - Walk " << pet.getName();
		cout << "| 5 - Pet " << pet.getName();
		cout << "| 6 - Display " << pet.getName() << "'s Current State ";
		cout << "| 7 - Return to pet menu" << endl;


		//causes delay for 1 second to ensure that timesince variables only increase every second
		sleep_for(seconds(1));
		//increase timesince variables by 1
		pet.setLastFed(pet.getLastFed() + 1);
		pet.setLastSlept(pet.getLastSlept() + 1);
		pet.setLastWalked(pet.getLastWalked() + 1);
		pet.setLastHydrated(pet.getLastHydrated() + 1);


		//React to users choice (through keyboard input) (0x8000 is used.... )
		if (GetKeyState('1') & 0x8000)
		{
			//Call function that feeds pet
			feedPet(pet);
		}
		else if (GetKeyState('2') & 0x8000)
		{
			//Call function that allows pet to sleep
			letPetSleep(pet);
		}
		else if (GetKeyState('3') & 0x8000)
		{
			//Call function that hydrates pet
			hydratePet(pet);
		}
		else if (GetKeyState('4') & 0x8000)
		{
			//Call function that walks pet
			walkPet(pet);
		}
		else if (GetKeyState('5') & 0x8000)
		{
			//Initialise random number generator
			srand(time(0));

			switch (pet.getHappiness())
			{
				//Pet is upset or angry
			case 0:
				//Choses random number between 7 and 8
				//Method: chooses random number between 0 and 1, then adds 7 to said number
				petReaction = rand() % 2 + 7;
				break;

			case 1:
				petReaction = rand() % 2 + 7;
				break;

				//Pet is contempt
			case 2:
				//Choses random number between 5 and 6
				//Method: chooses random number between 0 and 1, then adds 5 to said number
				petReaction = rand() % 2 + 5;
				break;

				//Pet is happy
			case 3:
				//Choses random number between 1 and 4 
				//Method: chooses random number between 0 and 3, then adds 1 to said number
				petReaction = rand() % 4 + 1;
				break;

			case 4:
				petReaction = rand() % 4 + 1;
				break;
			}


			//Call function that makes pet do a trick
			petPet(pet, petReaction);
		}
		else if (GetKeyState('6') & 0x8000)
		{
			//Displays pets status
			displayPetStatus(pet);
		}
		else if (GetKeyState('7') & 0x8000)
		{
			cout << "Returning to pet menu \n" << endl;

			//Delay for 3 seconds to let user read message
			sleep_for(seconds(3));

			leaveMenu = true;
		}


		//Make pet hungrier
		switch (pet.getLastFed())
		{
			//When it's been 5 minutes since fed
		case 300:
			//Pet is slightly peckish
			pet.setHunger(3);
			break;

			//When it's been 10 minutes since fed
		case 600:
			//Pet is fairly hungry
			pet.setHunger(2);
			break;

			//When it's been 15 minutes since fed
		case 900:
			//Pet is starving
			pet.setHunger(1);
			break;

			//When it's been 20 minutes since fed
		case 1200:
			//Pet is dead
			pet.setHunger(0);
			break;
		}


		//Make pet more tired
		switch (pet.getLastSlept())
		{
			//When it's been 5 minutes since pet last slept
		case 300:
			//Pet is awake
			pet.setTiredness(3);
			break;

			//When it's been 10 minutes since pet last slept
		case 600:
			//Pet is tired
			pet.setTiredness(2);
			break;

			//When it's been 15 minutes since pet last slept
		case 900:
			//Pet is falling asleep
			pet.setTiredness(1);
			break;

			//When it's been 20 minutes since pet last slept
		case 1200:
			//Pet has collapsed
			pet.setTiredness(0);
			break;
		}


		//Make pet less happy
		switch (pet.getLastWalked())
		{
			//When it's been 5 minutes since pet last walked
		case 300:
			//Pet is feeling lively
			pet.setBoredom(3);
			break;

			//When it's been 10 minutes since pet last walked
		case 600:
			//Pet is feeling interested
			pet.setBoredom(2);
			break;

			//When it's been 15 minutes since pet last walked
		case 900:
			//Pet is feeling bored
			pet.setBoredom(1);
			break;

			//When it's been 20 minutes since pet last walked
		case 1200:
			//Pet is feeling lethargic
			pet.setBoredom(0);
			break;
		}


		//Make pet thirstier
		switch (pet.getLastHydrated())
		{
			//When it's been 5 minutes since hydrated
		case 300:
			//Pet is fairly hydrated
			pet.setHydration(3);
			break;

			//When it's been 10 minutes since hydrated
		case 600:
			//Pet is thirsty
			pet.setHydration(2);
			break;

			//When it's been 15 minutes since hydrated
		case 900:
			//Pet is parched
			pet.setHydration(1);
			break;

			//When it's been 20 minutes since hydrated
		case 1200:
			//Pet is dead
			pet.setHydration(0);
			break;
		}

	}
	//Store pet attributes in a file
	writeToFile(pet.getHunger(), pet.getTiredness(), pet.getHydration(), pet.getBoredom());

}


void feedPet(petAttributes& pet)
{
	//If pet isn't full up 
	if (pet.getHunger() != 4)
	{
		//Feed pet
		pet.setHunger(4);

		//Display message
		cout << pet.getName() << " eats food" << endl;

		//Since pet is being fed, reset time since fed to 0
		pet.setLastFed(0);

		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
	//If pet is full up
	else
	{
		//Tell user
		cout << pet.getName() << " is well fed" << endl;

		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
}


void letPetSleep(petAttributes& pet)
{
	//If pet isn't wide awake
	if (pet.getTiredness() != 4)
	{
		//Let pet sleep
		pet.setTiredness(4);

		cout << pet.getName() << " goes to sleep" << endl;

		//Since pet is sleeping, reset time since slept to 0
		pet.setLastSlept(0);

		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
	//If pet is wide awake
	else
	{
		//Tell user
		cout << pet.getName() << " is wide awake" << endl;

		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
}


void hydratePet(petAttributes& pet)
{
	//If pet isn't hydrated 
	if (pet.getHydration() != 4)
	{
		//Give pet water
		pet.setHydration(4);

		cout << pet.getName() << " drinks water" << endl;

		//Since pet is being hydrated, reset time since hydrated to 0
		pet.setLastHydrated(0);


		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
	//If pet is hydrated
	else
	{
		//Tell user
		cout << pet.getName() << " is feeling hydrated" << endl;

		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
}


void walkPet(petAttributes& pet)
{
	//If pet isn't excited
	if (pet.getBoredom() != 4)
	{
		//Take pet for a walk
		pet.setBoredom(4);

		cout << pet.getName() << " goes for a walk" << endl;

		//Since pet is going for a walk, reset time since walked to 0
		pet.setLastWalked(0);

		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
	//If pet is excited
	else
	{
		//Tell user
		cout << pet.getName() << " is feeling excited" << endl;

		//Wait for 1 second then return to menu
		sleep_for(seconds(1));
	}
}


//Incorporate pets happiness level into reaction//
void petPet(petAttributes pet, int petReaction)
{
	switch (petReaction)
	{
	case 1:
		cout << pet.getName() << " rolls over" << endl;
		break;

	case 2:
		cout << pet.getName() << " wags tail" << endl;
		break;

	case 3:
		cout << pet.getName() << " lies down" << endl;
		break;

	case 4:
		cout << pet.getName() << " barks" << endl;
		break;

	case 5:
		cout << pet.getName() << " whines" << endl;
		break;

	case 6:
		cout << pet.getName() << " walks away" << endl;
		break;

	case 7:
		cout << pet.getName() << " growls aggressively" << endl;
		break;

	case 8:
		cout << pet.getName() << " snaps at hand" << endl;
		break;
	}

	//Wait for 1 second then return to menu
	sleep_for(seconds(1));

}


void displayPetStatus(petAttributes pet)
{
	
	cout << pet.getName() << " is " << pet.getHungerStatus() << endl;
	cout << pet.getName() << " is " << pet.getTirednessStatus() << endl;
	cout << pet.getName() << " is " << pet.getHappinessStatus() << endl;
	cout << pet.getName() << " is " << pet.getHydrationStatus() << endl;
	cout << pet.getName() << " is " << pet.getBoredomStatus() << endl;

	//Wait for 2 seconds then return to menu
	sleep_for(seconds(2));
}


int changeHappiness(petAttributes pet)
{

	//Calculate and return average of pet attributes
	return (pet.getHunger() + pet.getTiredness() + pet.getHydration() + pet.getBoredom()) / 4;

}



int readFromFile()
{
	int petData = 0;

	//Open file
	ifstream petDetails;

	petDetails.open(fileLocation, ios::in);

	//Read values from text document to pet variables
	petDetails >> petData;

	//Close file
	petDetails.close();

	return petData;
}


void writeToFile(int hunger, int tiredness, int hydration, int boredom)
{
	//Open file
	ofstream petDetails;
	petDetails.open(fileLocation, ios::out);

	//Write values from pet variables to text document
	petDetails << hunger << endl;
	petDetails << tiredness << endl;
	petDetails << hydration << endl;
	petDetails << boredom << endl;

	//Close file
	petDetails.close();
}