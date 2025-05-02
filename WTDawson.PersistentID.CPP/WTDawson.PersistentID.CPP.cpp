#include "pch.h"
#include "framework.h"
#include <cmath>

bool IsPrime(long num);
char ASCIIUpper[] = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
char ASCIILower[] = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
char ASCIINumerical[] = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
char ASCIISpecial[] = { '`', '¬', '¦', '!', '\"', '£', '$', '%', '^', '&', '*', '(', ')', '-', '=', '_', '+', '\\', '|', '[', ']', '{', '}', ';', '\'', '#', ':', '@', '~', ',', '.', '/', '<', '>', '?'};

int DeterministicIDBuilder::Algorithm::GenerateNumber(int iteration, int magic, int numChars, int charsTotal) {
	long prime = abs(magic * (numChars + charsTotal)) + 1;
	while (!IsPrime(prime)) prime += 2;
	return (int)(((iteration * prime + magic) ^ (iteration + magic * prime)) % numChars);
}

void DeterministicIDBuilder::SetRules(bool upper, bool lower, bool numerical, bool special) {
	this->Upper = upper;
	this->Lower = lower;
	this->Numerical = numerical;
	this->Special = special;
	this->Build();
}

void DeterministicIDBuilder::SetMagic(int magic) {
	this->Magic = magic;
	this->Build();
}

void DeterministicIDBuilder::SetLength(int length) {
	this->IDLength = length;
	this->Build();
}

void DeterministicIDBuilder::AddItems(char* Items[]) {
	char* _Items[(sizeof(this->Items) / sizeof(char)) +
		((sizeof(Items) / sizeof(char)))];
	for (int i = 0; i < sizeof(this->Items) / sizeof(char); i++)
	{
		_Items[i] = this->Items[i];
	}
	for (int i = 0; i < sizeof(Items) / sizeof(char); i++)
	{
		_Items[i + (sizeof(this->Items) / sizeof(char))] = Items[i];
	}
	this->Items = _Items;
}

void DeterministicIDBuilder::Build() {
	int size = sizeof(this->Items) / sizeof(char);
	if (this->Upper) size += sizeof(ASCIIUpper) / sizeof(char);
	if (this->Lower) size += sizeof(ASCIILower) / sizeof(char);
	if (this->Numerical) size += sizeof(ASCIINumerical) / sizeof(char);
	if (this->Special) size += sizeof(ASCIISpecial) / sizeof(char);

	char* chars = new char[size];

	int i = 0;
	int k = 0;
	int charTotal = 0;
	while (this->Items[i] != nullptr) {
		for (int j = 0; j < sizeof(this->Items[i]) / sizeof(char); j++)
		{
			chars[k] = this->Items[i][j];
			charTotal += (int)this->Items[i][j];
			k++;
		}
		i++;
	}

	if (this->Upper) {
		for (int i = 0; i < sizeof(ASCIIUpper) / sizeof(char); i++)
		{
			chars[k] = ASCIIUpper[i];
			charTotal += (int)ASCIIUpper[i];
			k++;
		}
	}

	if (this->Lower) {
		for (int i = 0; i < sizeof(ASCIILower) / sizeof(char); i++)
		{
			chars[k] = ASCIILower[i];
			charTotal += (int)ASCIILower[i];
			k++;
		}
	}

	if (this->Numerical) {
		for (int i = 0; i < sizeof(ASCIINumerical) / sizeof(char); i++)
		{
			chars[k] = ASCIINumerical[i];
			charTotal += (int)ASCIINumerical[i];
			k++;
		}
	}

	if (this->Special) {
		for (int i = 0; i < sizeof(ASCIISpecial) / sizeof(char); i++)
		{
			chars[k] = ASCIISpecial[i];
			charTotal += (int)ASCIISpecial[i];
			k++;
		}
	}

	char* id = new char[this->IDLength];
	for (int i = 0; i < this->IDLength; i++)
	{
		id[i] = chars[DeterministicIDBuilder::Algorithm::GenerateNumber(i, this->Magic, sizeof(chars) / sizeof(char), charTotal)];
	}

	this->ID = id;
}

char* DeterministicIDBuilder::ToString() {
	this->Build();
	return this->ID;
}

bool IsPrime(long num) {
	if (num < 2) return false;
	if (num % 2 == 0) return num == 2;

	for (long i = 3; i * i <= num; i += 2)
		if (num % i == 0) return false;

	return true;
}