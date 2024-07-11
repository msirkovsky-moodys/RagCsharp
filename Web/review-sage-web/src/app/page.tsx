'use client'
import styles from "./page.module.css";
import { RepoInput } from "./RepoInput";

export async function fetchData() {
  const prNumber = 1;
  const repoName = "msirkovsky-moodys/PullRequestReviewTest";
  const personalToken = "ghp_exampleToken123";

  return {
    prNumber,
    repoName,
    personalToken,
  };
}

export default async function Home() {

  const data = await fetchData();
  return (
    <main className={styles.main}>
      <div className={styles.description}>
        <h1>
          SageReview v.01
        </h1>
        <div>          
        </div>
      </div>

      <div className={styles.center}>
        <RepoInput {...data} />
      </div>

    
    </main>
  );
}
