#pragma once

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers

#ifndef DeterministicID

class DeterministicIDBuilder {
	/// <summary>
	/// The algorithm
	/// </summary>
	static class Algorithm {
	public:
		/// <summary>
		/// Generates the number using the algorithm.
		/// </summary>
		/// <param name="iteration">The iteration</param>
		/// <param name="magic">The "magic"</param>
		/// <param name="numChars">The total number of available characters</param>
		/// <param name="charsTotal">All of the available characters casted to an integer and then added together</param>
		/// <returns>The number</returns>
		static int GenerateNumber(int iteration, int magic, int numChars, int charsTotal);
	};

public:
	/// <summary>
	/// Sets up the DeterministicIDBuilder with the default values
	/// </summary>
	DeterministicIDBuilder() {
		SetRules();
		SetMagic();
		SetLength();
	}

	void SetRules(bool upper = true, bool lower = true, bool numerical = true, bool special = false);
	void SetMagic(int magic = 0);
	void SetLength(int length = 16);
	void Rebuild() { Build(); }
	void AddItems(char* Items[]);
	char* ToString();
	enum Version {
		V1 = 0
	};
	Version AlgorithmVersion = Version::V1;
private:
	bool Upper, Lower, Numerical, Special;
	int IDLength = 16;
	void Build();
protected:
	char** Items = {};
	char* ID = (char*)"";
	int Magic = 0;
};

#endif // !DeterministicID