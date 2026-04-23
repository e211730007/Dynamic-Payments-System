import React from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, Cell, LabelList } from 'recharts';

const COLORS = { 'Python': '#FF6B6B', '.NET': '#4D96FF', 'CSharp': '#4D96FF', 'SQL': '#6BCB77' };

const PerformanceChart = ({ data }) => {
    if (!data || data.length === 0) return <div style={{ textAlign: 'center', padding: '20px' }}>טוען גרף...</div>;

    return (
        <div style={{ backgroundColor: '#fff', padding: '20px', borderRadius: '15px', boxShadow: '0 4px 20px rgba(0,0,0,0.08)', marginBottom: '30px' }}>
            <h3 style={{ marginBottom: '20px', color: '#2D3436', textAlign: 'right' }}>השוואת זמני ביצוע (ms)</h3>
            <div style={{ width: '100%', height: '400px' }}> 
                <ResponsiveContainer width="100%" height="100%">
                    <BarChart data={data} margin={{ top: 20, right: 30, left: 0, bottom: 0 }}>
                        <CartesianGrid strokeDasharray="3 3" vertical={false} stroke="#f0f0f0" />
                        <XAxis dataKey="method" axisLine={false} tickLine={false} />
                        <YAxis axisLine={false} tickLine={false} />
                        <Tooltip cursor={{fill: '#f8f9fa'}} contentStyle={{ borderRadius: '10px', border: 'none' }} />
                        
                        <Bar dataKey="runTime" radius={[10, 10, 0, 0]} barSize={60}>
                            {data.map((entry, index) => (
                                <Cell key={`cell-${index}`} fill={COLORS[entry.method] || '#ADADB2'} />
                            ))}
                            <LabelList dataKey="runTime" position="top" formatter={(v) => v?.toFixed(3)} />
                        </Bar>
                    </BarChart>
                </ResponsiveContainer>
            </div>
        </div>
    );
};

export default PerformanceChart;