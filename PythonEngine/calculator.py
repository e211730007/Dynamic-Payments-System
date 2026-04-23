import pandas as pd
import pyodbc
import time
import numpy as np

server = 'DESKTOP-QSADO9D\\SQLEXPRESS'
database = 'DynamicPaymentsDB'
connection_string = f'DRIVER={{SQL Server}};SERVER={server};DATABASE={database};Trusted_Connection=yes;'

def run_python_engine():
    conn = None
    try:
        # 1. התחברות ל-SQL Server
        conn = pyodbc.connect(connection_string)
        print("[Python] Connected to Database successfully.")

        # 2. טעינת נתונים (100,000 שורות כדי להשוות לשיטות האחרות)
        print("[Python] Loading 100,000 rows into memory...")
        df_data = pd.read_sql("SELECT TOP 100000 a, b, c, d FROM t_data", conn)
        df_targil = pd.read_sql("SELECT * FROM t_targil", conn)

        print(f"[Python] Starting calculation for {len(df_targil)} formulas...")
        
        # התחלת מדידת זמן
        start_time = time.time()

        # 3. לולאה על הנוסחאות וחישוב דינמי בעזרת מנוע eval של Pandas
        for index, row in df_targil.iterrows():
            formula = row['targil']
            condition = row['tnai']
            formula_false = row['targil_false']
            targil_id = row['targil_id']

            # בדיקה האם קיים תנאי (נוסחת IF)
            if condition and str(condition).strip() and str(condition).lower() != 'none':
                # שימוש ב-numpy.where לביצוע חישוב מותנה ומהיר על כל הטבלה
                mask = df_data.eval(condition)
                df_data[f'res_{targil_id}'] = np.where(mask, df_data.eval(formula), df_data.eval(formula_false))
            else:
                # חישוב אריתמטי רגיל
                df_data[f'res_{targil_id}'] = df_data.eval(formula)

        # סיום מדידת זמן
        end_time = time.time()
        duration = end_time - start_time
        print(f"[Python] Success! Calculation Time: {duration:.4f} seconds.")

        # 4. שמירת התוצאה בטבלת הלוג (t_log) עם תיקון סוגי נתונים
        cursor = conn.cursor()
        
        # המרה מפורשת כדי למנוע שגיאת Invalid parameter type
        targil_id_to_save = int(df_targil.iloc[0]['targil_id'])
        run_time_to_save = float(duration)

        cursor.execute("""
            INSERT INTO t_log (targil_id, method, run_time) 
            VALUES (?, ?, ?)""", 
            (targil_id_to_save, 'Python', run_time_to_save)
        )
        
        conn.commit()
        print("[Python] Log entry saved to t_log table.")

    except Exception as e:
        print(f"[Python] Error occurred: {str(e)}")
    
    finally:
        if conn:
            conn.close()
            print("[Python] Connection closed.")

if __name__ == "__main__":
    run_python_engine()
    print("\n--- Python Engine Task Finished ---")
    input("Press Enter to exit...")