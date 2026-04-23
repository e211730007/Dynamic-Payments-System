import React, { useState, useEffect } from 'react';
import { getLogs } from '../api/LogService';
import PerformanceChart from '../components/PerformanceChart';
import RawDataTable from '../components/RawDataTable';

const Dashboard = () => {
    const [logs, setLogs] = useState([]);

    const loadData = async () => {
        const data = await getLogs();
        setLogs(data || []);
    };

    useEffect(() => { loadData(); }, []);

    const avgTime = logs.length > 0 
        ? (logs.reduce((acc, curr) => acc + (curr.runTime || 0), 0) / logs.length).toFixed(3) 
        : 0;

    return (
        <div style={{ backgroundColor: '#f4f7f6', minHeight: '100vh', padding: '40px', fontFamily: 'Arial', direction: 'rtl' }}>
            <header style={{ marginBottom: '40px', textAlign: 'center' }}>
                <h1 style={{ color: '#2d3436' }}>מערכת השוואת ביצועים</h1>
                <p style={{ color: '#636e72' }}>משרד החינוך - מבדק פיתוח</p>
            </header>

            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '20px', marginBottom: '30px' }}>
                <div style={cardStyle("#4D96FF")}><h4>ממוצע זמן (ms)</h4><p>{avgTime}</p></div>
                <div style={cardStyle("#6BCB77")}><h4>מצב שרת</h4><p>מחובר</p></div>
                <div style={cardStyle("#FF6B6B")}><h4>{`סה"כ רשומות`}</h4><p>{logs.length}</p></div>
            </div>

            <PerformanceChart data={logs} />
            <RawDataTable data={logs} />
        </div>
    );
};

const cardStyle = (color) => ({
    backgroundColor: '#fff', padding: '20px', borderRadius: '15px', 
    boxShadow: '0 4px 15px rgba(0,0,0,0.05)', borderRight: `5px solid ${color}`, textAlign: 'right'
});

export default Dashboard;