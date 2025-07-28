import { useState } from 'react';
import { calculateProbability } from './requests';

function App() {
  const [a, setA] = useState('');
  const [b, setB] = useState('');
  const [type, setType] = useState('CombinedWith');
  const [result, setResult] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);


  const handleCalculate = async () => {
    const { result, error } = await calculateProbability({ a, b, type });
    setResult(result ?? null);
    setError(error ? error : null);
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-xl font-bold mb-4">Probability Calculator</h1>

      <input
        className="border p-2 mb-2 w-full"
        placeholder="Enter probability A (0 - 1)"
        value={a}
        onChange={(e) => setA(e.target.value)}
      />

      <input
        className="border p-2 mb-2 w-full"
        placeholder="Enter probability B (0 - 1)"
        value={b}
        onChange={(e) => setB(e.target.value)}
      />

      <select
        className="border p-2 mb-2 w-full"
        value={type}
        onChange={(e) => setType(e.target.value)}
      >
        <option value="CombinedWith">CombinedWith (P(A)P(B))</option>
        <option value="Either">Either (P(A) + P(B) – P(A)P(B))</option>
      </select>

      <button className="bg-blue-600 text-white px-4 py-2" onClick={handleCalculate}>
        Calculate
      </button>

      {result !== null && <div className="mt-4">Result: <strong>{result}</strong></div>}
      {error && <div className="mt-4 text-red-600">{error}</div>}
    </div>
  );
}

export default App;