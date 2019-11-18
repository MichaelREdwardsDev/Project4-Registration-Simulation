//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 3 - Infix Postfix Conversion
//	File Name:		Utility.cs
//	Description:	Several common static functions, such as opening and reading a file, cleaning a list of tokens, etc
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Michael Edwards, edwardsmr@etsu.edu
//	Created:		Sunday November 10, 2019
//	Copyright:		Michael Edwards, 2019
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
/// <summary>
/// Utils
/// </summary>
namespace Utils {
	/// <summary>
	/// Several static methods encapsulated to be used in several project into a single class for easy transfer.
	/// </summary>
	static class Utility {
		/// <summary>
		/// User Input Methods - String OpenFile()
		/// </summary>
		#region UserInput
		/// <summary>
		///	Uses the OpenFileDialog class to allow the user to pick a file from the local system to be analyzed
		/// </summary>
		/// <returns>The location and name of the file</returns>
		public static String OpenFile() {
			OpenFileDialog OpenDlg = new OpenFileDialog();
			OpenDlg.Filter = "text files | *.txt;*.text";
			OpenDlg.InitialDirectory = Application.StartupPath.Replace(@"\bin\Debug", "");
			OpenDlg.Title = "Select the text file you wish to open...";
			if(OpenDlg.ShowDialog() == DialogResult.OK) {
				return OpenDlg.FileName;
			} else {
				return "ERROR";
			}
		}
		/// <summary>
		/// Reads the text of a file and returns it as a String
		/// </summary>
		/// <param name="filePath">The path to the file that the user selected</param>
		/// <returns>String obtained from the filereader reading the file</returns>
		public static String ReadFile(String filePath) {
			StreamReader reader = null;
			try {
				reader = new StreamReader(filePath);
				return reader.ReadToEnd();
			} catch(Exception ex) {
				Console.WriteLine(ex);
				return "";
			} finally {
				if(reader != null) {
					reader.Close();
				}
			}
		}
		#endregion
		/// <summary>
		/// Tokenize Methods - List<String> Tokenize(String original, char[] delimiters), void InsertTokens(List<String> tokens, int delmIndex, ref int i),
		///					   List<String> CleanList(List<String> tokens), List<String> WordsOnly(List<String> tokens)
		/// </summary>
		#region TokenizeMethods
		/// <summary>
		/// Seperates all tokens from the text, tokens being words, punctuation, new lines, etc
		/// </summary>
		/// <param name="original"> The originial string read from the file </param>
		/// <param name="delimiters"> A list of all of the delmiters that can determine the end of tokens other than spaces and new lines</param>
		/// <returns> A cleaned list of the tokens in the text </returns>
		public static List<String> Tokenize(String original, char[] delimiters) {
			List<String> tokens = new List<String>(); // The list to be returned
			if(original.Contains(" ")) // If there are spaces, assume several tokens
			{
				tokens = original.Split(' ').ToList<String>();
			} else // else, only one token to split here
			  {
				tokens.Add(original);
			}
			// check each token for delimiters
			for(int i = 0; i < tokens.Count; i++) {
				int delimIndex = tokens[i].IndexOfAny(delimiters); // If there are delimiters, split and add tokens accordingly
				while(delimIndex != -1) {
					InsertTokens(tokens, delimIndex, ref i);
					delimIndex = tokens[i].IndexOfAny(delimiters);
				}
			}
			return CleanList(tokens);
		}
		/// <summary>
		/// Inserts tokens into a list after the tokens have been split
		/// </summary>
		/// <param name="tokens">Tokens after being split by spaces</param>
		/// <param name="delimIndex">The index of the delimiter in the token</param>
		/// <param name="i">Reference to the index the tokens will be insterted to in the list</param>
		private static void InsertTokens(List<String> tokens, int delimIndex, ref int i) {
			String delimChar = tokens[i][delimIndex].ToString();
			String unpuncToken = tokens[i].Remove(delimIndex, 1);
			tokens.RemoveAt(i);
			// if the delimiter is at the beginning of the token
			if(delimIndex == 0) {
				tokens.Insert(i, delimChar);
				tokens.Insert(++i, unpuncToken);
			} else// else if the delimiter is anywhere else in the token
			  {
				tokens.Insert(i, unpuncToken);
				tokens.Insert(++i, delimChar);
			}
		}
		/// <summary>
		/// Removes all blank lines from text, and splits new lines from the attached token
		/// </summary>
		/// <param name="tokens">All tokens from the text</param>
		/// <returns> A List of tokens with no blank lines, and seperate new lines</returns>
		public static List<String> CleanList(List<String> tokens) {
			List<String> returnList = new List<String>(); // List to be returned

			// for each token
			foreach(String str in tokens) {
				if(str == "" || str == " ")// if the token is empty, do not add it
				{
					continue;
				} else if(str.Contains("\r\n") && str != "\r\n")// else if the string has a new line, split the new line with the content of the string
				  {
					String realStr = str.Replace("\r\n", "");
					returnList.Add("\r\n");
					returnList.Add(realStr);
				} else // else, add
				  {
					returnList.Add(str);
				}
			}
			return returnList;
		}
		/// <summary>
		/// Removes all non-word tokens from the list of tokens
		/// </summary>
		/// <param name="TokenizedList">list of all tokens from the text</param>
		/// <param name="Delimiters">the array of possible delimiters</param>
		/// <returns>a list of only word tokens from the text</returns>
		public static List<String> WordsOnly(List<String> TokenizedList, char[] Delimiters) {
			List<String> onlyWords = new List<String>(); // List to be returned
														 // check each token for delimiters
			foreach(String w in TokenizedList) {
				if(w.IndexOfAny(Delimiters) == -1 && w != "\r\n") // if the token does not have any, 
				{
					String newWord = w.Trim(new char[] { '(', ')', '.' }); // trim other symbols
					onlyWords.Add(newWord); // then add
				}
			}
			return onlyWords;
		}

		/// <summary>
		/// Converts all tokens into an appropriate string, converts new line sequences into a displayible "new line" string
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns> A string containing all of the converted versions of the tokens from the text </returns>
		public static String TokensToString(List<String> tokens) {
			String retStr = "";
			foreach(String str in tokens) {
				if(str == "\r\n")
					retStr += "<New-Line Character>\r\n";
				else
					retStr += str + "\r\n";
			}
			return retStr;
		}
		#endregion
		/// <summary>
		/// System Messages - void WelcomeMessage(String msg, String caption = "Computer Science 2210", String author = "Michael Edwards),
		///					  void GoodByeMessage(String msg = "Goodbye and thank you for using this program.")
		///					  void PressAnyKey(String strVerb = "continue...")
		///					  void EnterInfo(String prompt)
		/// </summary>
		#region SystemMessages
		/// <summary>
		/// Displays a welcome message for the program
		/// </summary>
		/// <param name="msg">The message to be displayed</param>
		/// <param name="caption">The caption to lable the program</param>
		/// <param name="author">The name of the author of the program</param>
		public static void WelcomeMessage(String msg, String caption = "Computer Science 2210", String author = "Michael Edwards") {
			Console.WriteLine($"{msg,30}\n{caption,31}\n{author,28}");
		}
		/// <summary>
		/// Displays a goodbye message
		/// </summary>
		/// <param name="msg">The message to be displayed</param>
		public static void GoodbyeMessage(String msg = "Goodbye and thank you for using this program.") {
			MessageBox.Show("Thank you for using this program.", "Goodbye!", MessageBoxButtons.OK);
		}
		/// <summary>
		/// Displays a message to prompt the user to exit the program, as well as the key read used to continue past the pause
		/// </summary>
		/// <param name="strVerb">The prompt to continue</param>
		public static void PressAnyKey(String strVerb = "continue...") {
			Console.WriteLine("Press any to to exit the program...");
			Console.ReadKey();
		}
		/// <summary>
		/// Prompt the user to enter any info needed
		/// </summary>
		/// <param name="prompt">The prompt to be displayed</param>
		public static void EnterInfo(String prompt) {
			Console.WriteLine(prompt);
		}

		public static void ClearFormControls(Form form) {
			foreach(Control control in form.Controls) {
				if(control is TextBox) {
					(control as TextBox).Text = string.Empty;
				} else if(control is CheckBox) {
					(control as CheckBox).Checked = false;
				} else if(control is RadioButton) {
					(control as RadioButton).Checked = false;
				} else if(control is DateTimePicker) {
					(control as DateTimePicker).Value = DateTime.MinValue;
				} else if(control is ListBox) {
					(control as ListBox).Items.Clear();
				}
			}
			#endregion
		}
	}
}
