'use client';
import { Editor } from '@monaco-editor/react';
import { useState, useRef } from 'react';

const API_URL = 'http://localhost:5147';

export default function Home() {
  const [code, setCode] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [output, setOutput] = useState<string>('');
  const fileInputRef = useRef<HTMLInputElement>(null);

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

  const handleNewFile = () => {
    setCode('');
    setError('');
    setOutput('');
  };

  const handleOpenFile = () => {
    fileInputRef.current?.click();
  };

  const handleFileSelect = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e) => {
        setCode(e.target?.result as string);
      };
      reader.readAsText(file);
    }
  };

  const handleSaveFile = () => {
    const blob = new Blob([code], { type: 'text/plain' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'code.glt';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
  };  

  return (
    <div className='flex flex-col items-center justify-center h-screen p-4 bg-gray-100'>
      <div className='w-full max-w-4xl'>
      <button
            className='p-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition'
            onClick={handleNewFile}
          >
            Nuevo Archivo
          </button>
          <button
            className='p-2 bg-yellow-500 text-white rounded-md hover:bg-yellow-600 transition'
            onClick={handleOpenFile}
          >
            Abrir Archivo
          </button>
          <button
            className='p-2 bg-purple-500 text-white rounded-md hover:bg-purple-600 transition'
            onClick={handleSaveFile}
          >
            Guardar Archivo
          </button>
        </div>
        <input
          type="file"
          ref={fileInputRef}
          onChange={handleFileSelect}
          accept=".glt"
          className="hidden"
        />

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
    
  );
}

