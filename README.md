📊 Dynamic Payments Calculation System
 
מערכת לחישוב נוסחאות דינמיות על בסיס נתונים רחב (1,000,000 רשומות) תוך השוואת ביצועים בין מנועי חישוב שונים.

📁 מבנה הפרויקט (Hierarchy)
Documentation_and_Summary.pdf - דוח מסכם, ניתוח תוצאות ומסקנות.

Payments-ui/ - ממשק Dashboard ב-React להצגת השוואת זמני הביצוע.

Payments.API/ - שרת ASP.NET Core המספק נתונים מה-DB וניהול לוגיקה.

DynamicPaymentCalculator/ - פרויקט הקונסול המרכזי.

Note: מימוש שלושת מנועי החישוב (C#, Python Integration, and SQL Stored Procedure) נמצא בקובץ Program.cs בתוך פרויקט זה.

PythonEngine/ - סקריפט ה-Python המשמש את המנוע לחישוב דינמי.

Scripts/ - סקריפטים של SQL להקמת טבלאות, טעינת נתונים ויצירת הפרוצדורה.

🚀 הרצה מהירה
DB: הרץ את הסקריפטים שבתיקיית Scripts להקמת מסד הנתונים.

---
## 📊 Performance Analysis Results
כחלק מהמבדק, בוצעה השוואה בין שלושת המנועים על פני 1,000,000 רשומות. להלן התוצאות:

| Engine | Execution Time | Efficiency |
| :--- | :--- | :--- |
| **SQL Stored Procedure** | ~120ms | Best for large data |
| **C# (.NET) Engine** | ~450ms | Good for complex logic |
| **Python Engine** | ~1.2s | High flexibility, lower speed |

### מסקנה מרכזית:
שימוש ב-SQL בצד השרת סיפק את הביצועים המהירים ביותר בצורה משמעותית.

מכיוון שהקובץ דוח צסכם אינו נפתח תמיד תקין דרך הגיט ניתן להוריד אותו
> ניתן להוריד את הדוח המלא והמפורט הכולל גרפים וניתוח מעמיק כאן: [Documentation_and_Summary.pdf](./Documentation_and_Summary.pdf)
> 

Server: הרץ את פרויקט ה-API מה-Solution ב-Visual Studio.

Client: בתוך תיקיית Payments-ui, הרץ npm install ולאחר מכן npm start.

💡 מסקנות עיקריות
המבדק הוכיח כי SQL Stored Procedures הם הפתרון המהיר ביותר לעיבוד מאסיבי של נתונים, בעוד ש-Python מהירה מאוד בחישוב רשומה בודדת אך פחות יעילה במאסה של מיליון רשומות בשל תקורת קריאות.
