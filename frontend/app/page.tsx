'use client';
import { Editor } from '@monaco-editor/react';
import { useState } from 'react';

const API_URL = 'http://localhost:5147';

export default function Home() {
  const [code, setCode] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [output, setOutput] = useState<string>('');

  const handleExecute = async () => {
    try {
      const response = await fetch(`${API_URL}/compile`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ code }),
      });

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.error || 'Error desconocido');
      }

      setOutput(data.result);
      setError('');
    } catch (err) {
      setOutput('');
      setError(err instanceof Error ? err.message : 'Error desconocido');
    }
  };

  return (
    <div className='flex flex-col items-center justify-center h-screen p-4 bg-gray-100'>
      <div className='w-full max-w-4xl'>
        <Editor
          height='60vh'
          defaultLanguage='javascript'
          theme='vs-dark'
          value={code}
          onChange={(value) => setCode(value || '')}
        />
        <button
          className='mt-4 p-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition'
          onClick={handleExecute}
        >
          Ejecutar
        </button>
        {output && (
          <div className='mt-4 p-4 bg-white rounded-md shadow-md w-full max-h-60 overflow-auto'>
            <h1 className='text-lg font-bold mb-2'>Output</h1>
            <pre className='whitespace-pre-wrap text-black'>{output}</pre>
          </div>
        )}
        {error && (
          <div className='mt-4 p-4 bg-red-100 text-red-700 rounded-md shadow-md w-full max-h-60 overflow-auto'>
            <h1 className='text-lg font-bold mb-2'>Error</h1>
            <pre className='whitespace-pre-wrap'>{error}</pre>
          </div>
        )}
      </div>
    </div>
  );
}

