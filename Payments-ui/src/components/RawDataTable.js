import React from 'react';

const RawDataTable = ({ data }) => {
    return (
        <div style={{ backgroundColor: '#fff', borderRadius: '15px', boxShadow: '0 4px 20px rgba(0,0,0,0.08)', overflow: 'hidden' }}>
            <div style={{ padding: '20px', backgroundColor: '#f8f9fa', borderBottom: '1px solid #eee' }}>
                <h3 style={{ margin: 0, color: '#2D3436', textAlign: 'right' }}>נתוני הרצה מהמסד</h3>
            </div>
            <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'right' }} dir="rtl">
                <thead>
                    <tr style={{ backgroundColor: '#f8f9fa', color: '#636e72' }}>
                        <th style={pStyle}>מזהה</th>
                        <th style={pStyle}>מנוע חישוב</th>
                        <th style={pStyle}>תוצאה</th>
                        <th style={pStyle}>זמן (ms)</th>
                        <th style={pStyle}>תאריך יצירה</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map((log) => (
                        <tr key={log.id} style={{ borderBottom: '1px solid #f1f1f1' }}>
                            <td style={pStyle}>#{log.id}</td>
                            <td style={pStyle}>
                                <span style={badgeStyle(log.method)}>{log.method}</span>
                            </td>
                            <td style={pStyle}>{log.calculationResult?.toLocaleString()}</td>
                            <td style={{ ...pStyle, fontWeight: 'bold' }}>{log.runTime?.toFixed(4)}</td>
                            <td style={{ ...pStyle, fontSize: '12px', color: '#95a5a6' }}>
                                {log.createdAt ? new Date(log.createdAt).toLocaleString('he-IL') : '-'}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

const pStyle = { padding: '15px 20px' };
const badgeStyle = (m) => ({
    padding: '4px 12px', borderRadius: '20px', fontSize: '11px', fontWeight: 'bold',
    backgroundColor: m === 'Python' ? '#fff0f0' : m === '.NET' ? '#eef5ff' : '#f0fff4',
    color: m === 'Python' ? '#FF6B6B' : m === '.NET' ? '#4D96FF' : '#6BCB77'
});

export default RawDataTable;