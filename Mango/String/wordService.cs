using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.String
{
    public sealed class wordService
    {
        private string text;
        private int wordCount;
        private int sentenceCount;
        private int paragraphCount;

        public int updateWordCount(string enteredText)
        {
            text = enteredText;
            string textToUse = enteredText.Replace("\r", " ");
            string[] words = textToUse.Split(' ');
            wordCount = words.Count();
            int emptyWords = 0;
            for (int i = 0; i < wordCount; i++)
            {
                if (words[i] == "")
                {
                    emptyWords += 1;
                }
            }
            wordCount -= emptyWords;
            return wordCount;
        }

        public int getWordCount()
        {
            if (text == null)
            {
                wordCount = 0;
            }
            return wordCount;
        }

        public int getSentenceCount()
        {
            try
            {
                string[] splitter = new string[] { ".", "?", "!" };
                string[] sentences = text.Split(splitter, StringSplitOptions.None);
                sentenceCount = sentences.Count();
                int blankSentences = 0;
                for (int i = 0; i < sentenceCount; i++)
                {
                    if (sentences[i] == "")
                    {
                        blankSentences += 1;
                    }
                }

                sentenceCount -= blankSentences;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                sentenceCount = 0;
            }

            return sentenceCount;
        }

        public int getParagraphCount()
        {

            try
            {
                string[] splitter = new string[] { "\r\r" };
                string[] paragraphs = text.Split(splitter, StringSplitOptions.None);

                paragraphCount = paragraphs.Count();
                int blankParagraphs = 0;
                for (int i = 0; i < paragraphCount; i++)
                {
                    if (paragraphs[i] == "")
                    {
                        blankParagraphs += 1;
                    }
                }
                paragraphCount -= blankParagraphs;
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                sentenceCount = 0;
            }


            return paragraphCount;

        }
    }
}
